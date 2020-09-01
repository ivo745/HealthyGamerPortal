using Discord.Rest;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using HealthyGamerPortal.Common.Helpers;
using HealthyGamerPortal.Common.ViewModels.Calendar;
using HealthyGamerPortal.Models;
using HealthyGamerPortal.WEB.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers
{
    public class CalendarController : BaseController
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly string calanderId = @"o25ue1mi0sb2c1drl81j9amoa4@group.calendar.google.com";
        private static CalendarService _calendarService;

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
            if (_calendarService == null)
            {
                string jsonFile = "hgcore-de197768f76b.json";
                string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents };
                ServiceAccountCredential credential;
                using (var stream = new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
                {
                    var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                    credential =
                        new ServiceAccountCredential(
                        new ServiceAccountCredential.Initializer(confg.ClientEmail)
                        {
                            Scopes = Scopes
                        }.FromPrivateKey(confg.PrivateKey));
                }

                _calendarService = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                });
            }
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult AddEvent([FromForm] EventViewModel model)
        {
            EventDateTime newDate = new EventDateTime() { DateTime = model.Date };
            Event newEvent = new Event()
            {
                Start = newDate,
                End = newDate,
                Summary = model.Attendee
            };
            _calendarService.Events.Insert(newEvent, calanderId).Execute();

            return Json(new { newEvent });
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult UpdateEvent([FromForm] EventViewModel model)
        {
            EventDateTime newDate = new EventDateTime() { DateTime = model.Date };
            Event newEvent = new Event()
            {
                Start = newDate,
                End = newDate,
                Summary = model.Attendee
            };
            _calendarService.Events.Update(newEvent, calanderId, model.Id).Execute();

            return Json(new { newEvent });
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult AddEvent()
        {
            string message = String.Empty;
            int eventId = 0;

            return Json(new { message, eventId });
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> MemberDetails(ulong Id)
        {
            return PartialView("_MemberDetails", new GuildUserItemViewModel());
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult DeleteEvent([FromBody] Event evt)
        {
            string message = String.Empty;

            //message = _DA.DeleteEvent(evt.EventId);

            return Json(new { message });
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index(CalendarViewModel model)
        {
            model.discordMembers = new List<DiscordMember>();
            if (model.guildId > 0)
            {
                var guild = await HttpClientHelper.ClientBot().GetGuildAsync(model.guildId) as RestGuild;
                var users = await guild.GetUsersAsync().FirstOrDefault();
                if (users != null)
                {
                    foreach (var item in users)
                    {
                        var avatar = item.GetAvatarUrl(Discord.ImageFormat.Auto, 64);
                        if (string.IsNullOrEmpty(avatar))
                            avatar = item.GetDefaultAvatarUrl();

                        var member = new DiscordMember
                        {
                            Id = item.Id,
                            Name = item.Username,
                            Avatar = avatar
                        };
                        model.discordMembers.Add(member);
                    }
                }
                return View(model);
            }

            return View(new CalendarViewModel());
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new CalendarViewModel()
            {
                discordMembers = new List<DiscordMember>(),
            };

            var allGuilds = await HttpClientHelper.ClientBot().GetGuildsAsync();
            if (allGuilds.Count > 0)
            {
                model.guildList = new List<SelectListItem>();
                foreach (var guildItem in allGuilds)
                {
                    model.guildList.Add(new SelectListItem { Text = guildItem.Name, Value = guildItem.Id.ToString() });
                }
            }
            return View(model);
        }
    }
}
