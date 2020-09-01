using HealthyGamerPortal.Common.Helpers;
using HealthyGamerPortal.Common.ViewModels.Guild;
using HealthyGamerPortal.Common.ViewModels.Guilds;
using HealthyGamerPortal.WEB.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyGamerPortal.WEB.Controllers
{
    public class GuildController : BaseController
    {
        private static ulong currentGuildId;

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create()
        {
            return PartialView("_Create", new CreateGuildItemViewModel());
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Create(CreateGuildItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reg = await HttpClientHelper.ClientBot().GetVoiceRegionsAsync();
                await HttpClientHelper.ClientBot().CreateGuildAsync(model.Name, reg.FirstOrDefault());
            }
            return PartialView("_Create", model);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var allGuilds = await HttpClientHelper.ClientBot().GetGuildsAsync();
            return View(allGuilds);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(ulong Id)
        {
            currentGuildId = Id;
            var result = await HttpClientHelper.ClientBot().GetGuildAsync(Id);
            var listy = new List<GuildUserViewModel>
            {
                new GuildUserViewModel() { Id = HttpClientHelper.CurrentUser().Id, Name = HttpClientHelper.CurrentUser().Username, Avatar = HttpClientHelper.CurrentUser().GetAvatarUrl() }
            };
            var model = new EditGuildItemViewModel { Id = Id, Name = result.Name, guildUsers = listy };
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task AddUser(GuildUserViewModel model)
        {
            var result = await HttpClientHelper.ClientBot().GetGuildAsync(currentGuildId);
            await result.AddGuildUserAsync(model.Id, "QqLvQoCW7LMLZudrRgosrSRt9HXdiW");
            TempData.Add("success-msg", "User has not been fousdf");
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(EditGuildItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                await HttpClientHelper.ClientBot().GetGuildAsync(model.Id).Result.UpdateAsync();
                TempData.Add("success-msg", "Invite sent to user.");
                return PartialView("_Edit", model);
            }
            return PartialView("_Edit", new EditGuildItemViewModel());
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Delete(ulong Id)
        {
            var response = await HttpClientHelper.ClientBot().GetGuildAsync(Id);
            var model = new DeleteGuildItemViewModel() { Id = Id, Name = response.Name };
            TempData.Add("modal-success", "aususu");
            return PartialView("_Delete", model);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Delete(DeleteGuildItemViewModel model)
        {
            await HttpClientHelper.ClientBot().GetGuildAsync(model.Id).Result.DeleteAsync();
            return PartialView("_Delete", null);
        }
    }
}