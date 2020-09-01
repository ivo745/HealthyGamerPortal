using HealthyGamerPortal.Common.ViewModels.Company;
using HealthyGamerPortal.WEB.Controllers.Base;
using HealthyGamerPortal.WEB.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers
{
    public class CompanyController : BaseController
    {
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        //[Authorize(AuthenticationSchemes = "Cookies, OpenIdConnect", )]
        public async Task<IActionResult> Index()
        {
            var api = Refit.RestService.For<IHealthyGamerPortalCompanyApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var model = await api.GetManyCompanyBuilding();
            return View(model.Result);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new CreateCompanyBuildingViewModel();
            return PartialView("_Create", model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create(CreateCompanyBuildingViewModel model)
        {
            //TempData[model.ContactName] = "ERROR";
            return PartialView("_Create", model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(long Id)
        {
            var model = new CreateCompanyBuildingViewModel();
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(CreateCompanyBuildingViewModel model)
        {
            return PartialView("_Edit", model);
        }
    }
}