using AuthCenter.Domain;
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
            return View(new LoginInfo { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            if (UserService.Users.Any(item =>
                item.Username == loginInfo.Username &&
                item.Password == loginInfo.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, loginInfo.Username),
                    new Claim(JwtClaimTypes.Name, loginInfo.Username)
                };

                var userIdentity = new ClaimsIdentity(claims, "password");
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                // 確保 Authentication Scheme 正確
                await HttpContext.SignInAsync("Cookies", userPrincipal);

                return Redirect(loginInfo.ReturnUrl ?? "/");
            }

            loginInfo.Message = "登入失敗，請確認帳號密碼";
            return View(loginInfo);
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
}
