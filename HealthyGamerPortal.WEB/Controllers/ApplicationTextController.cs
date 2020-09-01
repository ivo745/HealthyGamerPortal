using HealthyGamerPortal.Common.ViewModels.LocalizationRecords;
using HealthyGamerPortal.WEB.Controllers.Base;
using HealthyGamerPortal.WEB.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers
{
    public class ApplicationTextController : BaseController
    {
        /// <summary>
        /// Overview of all LocalizationRecords from the portal <see cref="List{LocalizationRecordViewModel}"/>
        /// </summary>
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var api = RestService.For<IHealthyGamerLocalizationRecordsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var model = await api.GetManyLocalizationRecords();
            return View(model.Result);
        }

        /// <summary>
        /// gets a LocalizationRecord byId <see cref="LocalizationRecordViewModel"/>
        /// </summary>
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var api = RestService.For<IHealthyGamerLocalizationRecordsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var response = await api.GetSingleLocalizationRecordById(Id);
            return PartialView("_Edit", response.Result);
        }

        /// <summary>
        /// Update a LocalizationRecord <see cref="LocalizationRecordViewModel"/>
        /// </summary>
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(LocalizationRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var api = RestService.For<IHealthyGamerLocalizationRecordsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var response = await api.EditSingleLocalizationRecord(model);
                if (response.Result)
                {
                }
            }
            return PartialView("_Edit", model);
        }
    }
}