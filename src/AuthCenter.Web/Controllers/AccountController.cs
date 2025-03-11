using AuthCenter.Service;
using Duende.IdentityModel;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthCenter.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService identityServerInteractionService;
        private readonly IClientStore clientStore;
        private readonly IEventService eventService;

        public AccountController(
            IIdentityServerInteractionService identityServerInteractionService,
            IClientStore clientStore,
            IEventService eventService)
        {
            this.identityServerInteractionService = identityServerInteractionService;
            this.clientStore = clientStore;
            this.eventService = eventService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginInputModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            // TODO: 註冊時將使用者輸入的密碼用某種不可還原的方式加密，登入時將使用者輸入的密碼用相同的方式加密在比對？
            if (UserService.Users.Any(item => 
                item.Username == model.Username && 
                item.Password == model.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, model.Username),
                    new Claim(JwtClaimTypes.Name, model.Username)
                };

                var userIdentity = new ClaimsIdentity(claims, "password");
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                // 確保 Authentication Scheme 正確
                await HttpContext.SignInAsync("Cookies", userPrincipal);

                return Redirect(model.ReturnUrl ?? "/");
            }

            ModelState.AddModelError("", "登入失敗，請確認帳號密碼");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await HttpContext.SignOutAsync("Cookies");

            var logoutRequest = await identityServerInteractionService.GetLogoutContextAsync(logoutId);

            if (!string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
                return Redirect(logoutRequest.PostLogoutRedirectUri);

            return RedirectToAction("Login");
        }
    }

    public class LoginInputModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
