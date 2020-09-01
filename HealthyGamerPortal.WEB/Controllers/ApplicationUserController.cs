using HealthyGamerPortal.Common.ViewModels.AuthorizationManagement;
using HealthyGamerPortal.Common.ViewModels.Users;
using HealthyGamerPortal.WEB.Controllers.Base;
using HealthyGamerPortal.WEB.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers
{
    public class ApplicationUserController : BaseController
    {
        private readonly IStringLocalizer<ApplicationUserController> _stringLocalizer;

        public ApplicationUserController(IStringLocalizer<ApplicationUserController> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create()
        {
            return View(new CreateApplicationUserViewModel());
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create(CreateApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var api = RestService.For<IHealthyGamerPortalUserApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var response = await api.CreateSingleApplicationUser(model);
                if (response.Result)
                {
                    TempData.Add("success-msg", "User has been created");
                    return RedirectToAction("Index", "ApplicationUser");
                }
                ModelState.AddModelError("Email", "This email has already been registered");
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> AddRole()
        {
            var model = new AuthorizationManagementViewModel() { };
            var datax = new List<Common.ViewModels.Company.CreateCompanyBuildingViewModel>();
            var data = new List<string>();
            var d1 = "PwerShellUser";
            var d2 = "Admin";
            var d3 = "Manager";
            data.Add(d1); data.Add(d2); data.Add(d3);
            model.SelectList1 = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Name");
            return PartialView("_AddRole", model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> AddRole(CreateApplicationUserViewModel model)
        {
            return PartialView("_AddRole", model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> AddEntrance()
        {
            var model = new AuthorizationManagementViewModel() { };
            var datax = new List<Common.ViewModels.Company.CreateCompanyBuildingViewModel>();
            var data = new List<string>();
            var d1 = "Create_port";
            data.Add(d1);
            model.SelectList1 = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Name");
            return PartialView("_AddEntrance", model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> AddEntrance(CreateApplicationUserViewModel model)
        {
            return PartialView("_AddEntrance", model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> AddProfile()
        {
            var model = new AuthorizationManagementViewModel() { };
            var datax = new List<Common.ViewModels.Company.CreateCompanyBuildingViewModel>();
            var data = new List<string>();
            var d1 = "Owner";
            var d2 = "Admin";
            var d3 = "Coach";
            data.Add(d1); data.Add(d2); data.Add(d3);
            model.SelectList1 = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(data, "Name");
            return PartialView("_AddProfile", model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> AddProfile(AutherizationMangementProfileViewModel model)
        {
            return PartialView("_AddProfile", model);
        }

        /// <summary>
        /// Overview of all users from the portal <see cref="IEnumerable{ApplicationUserViewModel}"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var api = RestService.For<IHealthyGamerPortalUserApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var model = await api.GetManyUsers();
            return View(model.Result);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var api = RestService.For<IHealthyGamerPortalUserApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var response = await api.GetSingleEditApplicationUserById(Id);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                //TempData["error-msg"] = _stringLocalizer[response.ErrorCode.ToString()];
                //var test = _stringLocalizer["UsersButtonText"];
                //TempData.Add("error-msg", _stringLocalizer[response.ErrorCode.ToString()]);
            }
            return View(response.Result);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(EditApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var api = RestService.For<IHealthyGamerPortalUserApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var response = await api.EditSingleApplicationUser(model);
                if (response.Result)
                {
                    TempData.Add("success-msg", "User has been Edite");
                    return RedirectToAction("Index", "ApplicationUser");
                }

                ModelState.AddModelError("Email", "This email has already been registered");
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var api = RestService.For<IHealthyGamerPortalUserApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var response = await api.GetSingleDeleteApplicationUserById(Id);
            if (response.Result != null)
            {
                return PartialView("_Delete", response.Result);
            }
            else
            {
                TempData.Add("error-msg", "User has not been found.");
                return null;
            }
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Delete(DeleteApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var api = RestService.For<IHealthyGamerPortalUserApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var response = await api.DeleteSingleApplicationUser(model);
                if (response.Result)
                {
                }
            }
            return PartialView("_Delete", model);
        }
    }
}