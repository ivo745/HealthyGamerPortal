using Discord.OAuth2;
using HealthyGamerPortal.Common.Cryptography;
using HealthyGamerPortal.Common.Enums;
using HealthyGamerPortal.Common.ViewModels.Login;
using HealthyGamerPortal.WEB.Controllers.Base;
using HealthyGamerPortal.WEB.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Presents the login form.
        /// </summary>
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<IActionResult> Oauth(BasicLoginViewModel model)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        /// <summary>
        /// Issues a call to authenticate with Discord middleware.
        /// </summary>
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public ChallengeResult Oauth(string redirectUri)
        {
            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUri },
                DiscordDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Performs account type check and redirect based on login username.
        /// </summary>
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<IActionResult> Login(BasicLoginViewModel model)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                var api = RestService.For<IHealthyGamerPortalAccountApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var response = await api.IsBasicAccount(new EncryptedMessage() { Length = model.Email.Length, Text = Rfc7905.EncryptText(model.Email) });

                if (response.Result == AccountType.Discord)
                {
                    return Oauth(Url.Action("Oauth", "Account"));
                }
                else
                {
                    // Response.StatusCode = 403; //prevents browsers from trying to remember a password when the login failed.
                    return RedirectToAction("BasicLogin", "Account", model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Presents basic login form
        /// </summary>
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<IActionResult> BasicLogin(BasicLoginViewModel model)
        {
            return View(model);
        }

        /// <summary>
        /// Performs Basic Authentication post, this is the landing point for basic authentication.
        /// </summary>
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<IActionResult> BasicAuth()
        {
            BasicLoginViewModel model = new BasicLoginViewModel();

            return View(model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Logout()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            return RedirectToAction("Oauth");
        }
    }
}