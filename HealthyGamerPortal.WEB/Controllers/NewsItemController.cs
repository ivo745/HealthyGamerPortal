using HealthyGamerPortal.Common.ViewModels.News;
using HealthyGamerPortal.WEB.Controllers.Base;
using HealthyGamerPortal.WEB.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers
{
    public class NewsItemController : BaseController
    {
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create()
        {
            return PartialView("_Create", new CreateNewsItemViewModel());
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create(CreateNewsItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var api = RestService.For<IHealthyGamerPortalNewsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var response = await api.CreateSingleNews(model);
                if (response.Result)
                {
                }
            }
            return PartialView("_Create", model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var api = RestService.For<IHealthyGamerPortalNewsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var model = await api.GetManyNews();
            return View(model.Result);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var api = RestService.For<IHealthyGamerPortalNewsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var model = await api.GetSingleEditNewsById(Id);
            return PartialView("_Edit", model.Result);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(EditNewsItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var api = RestService.For<IHealthyGamerPortalNewsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var response = await api.EditSingleNews(model);
                if (response.Result)
                {
                }
            }
            return PartialView("_Edit", model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var api = RestService.For<IHealthyGamerPortalNewsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var model = await api.GetSingleDeleteNewsById(Id);
            return PartialView("_Delete", model.Result);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Delete(DeleteNewsItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var api = RestService.For<IHealthyGamerPortalNewsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var response = await api.DeleteSingleNews(model);
                if (response.Result)
                {
                }
            }
            return PartialView("_Delete", model);
        }
    }
}