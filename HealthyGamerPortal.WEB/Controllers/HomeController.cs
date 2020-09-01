using Discord.Rest;
using HealthyGamerPortal.Common.Helpers;
using HealthyGamerPortal.Common.ViewModels.Guild;
using HealthyGamerPortal.Common.ViewModels.Home;
using HealthyGamerPortal.Common.ViewModels.News;
using HealthyGamerPortal.WEB.Controllers.Base;
using HealthyGamerPortal.WEB.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel()
            {
                NewsItemShortList = new List<NewsItemShortViewModel>(),
                GuildItemList = new List<GuildItemViewModel>(),
            };

            var api = RestService.For<IHealthyGamerPortalNewsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var response = await api.GetManyShortNews();

            foreach (var item in response.Result)
            {
                var newsItemShort = new NewsItemShortViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Item = item.Item,
                    DateCreated = item.DateCreated
                };
                model.NewsItemShortList.Add(newsItemShort);
            }

            var allGuilds = await HttpClientHelper.ClientBot().GetGuildsAsync();
            foreach (var guildItem in allGuilds)
            {
                string icon = string.IsNullOrEmpty(guildItem.IconUrl) ? "/Images/HG/Logos/logo-small.png" : guildItem.IconUrl;
                model.GuildItemList.Add(new GuildItemViewModel { Name = guildItem.Name, Id = guildItem.Id.ToString(), Icon = icon });
            }
            return View(model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GuildDetails(ulong Id)
        {
            RestGuild singleGuild = await HttpClientHelper.ClientBot().GetGuildAsync(Id);
            return PartialView("_GuildDetails", singleGuild);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create([FromBody]RestGuild model)
        {
            //TempData[model.ContactName] = "ERROR";
            return PartialView("_Create", model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> NewsDetails(Guid Id)
        {
            var api = RestService.For<IHealthyGamerPortalNewsApi>(new HttpClient(new Helpers.AuthenticatedHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
            var response = await api.GetSingleNewsById(Id);
            return PartialView("_NewsDetails", response.Result);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> NewsDetails(HomeViewModel model)
        {
            return PartialView("_NewsDetails", model);
        }
    }
}