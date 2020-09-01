using HealthyGamerPortal.Common.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers.Base
{
    public class BaseController : Controller
    {
        //public const string BaseUrl = "https://healthygamerportaldev-api.azurewebsites.net";
        public const string BaseUrl = "https://localhost:5001";

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = System.DateTimeOffset.UtcNow.AddYears(1), IsEssential = true, Secure = true, SameSite = SameSiteMode.Strict }
                );
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (feature != null)
                TempData.Add("error-msg", feature.Error.Message);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}