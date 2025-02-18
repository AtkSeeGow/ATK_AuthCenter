using Duende.IdentityModel;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AuthCenter.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly IClientStore _clientStore;
        private readonly IEventService _eventService;

        public AccountController(
            IIdentityServerInteractionService identityServerInteractionService,
            IClientStore clientStore,
            IEventService eventService)
        {
            _identityServerInteractionService = identityServerInteractionService;
            _clientStore = clientStore;
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginInputModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (model.Username == "admin" && model.Password == "1234")
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, model.Username),
                    new Claim(JwtClaimTypes.Name, model.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync("Cookies", claimsPrincipal);

                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return Redirect("~/connect/authorize/callback");
            }

            ModelState.AddModelError("", "登入失敗，請確認帳號密碼");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await HttpContext.SignOutAsync("Cookies");

            var logoutRequest = await _identityServerInteractionService.GetLogoutContextAsync(logoutId);

            if (!string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return Redirect(logoutRequest.PostLogoutRedirectUri);
            }

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
