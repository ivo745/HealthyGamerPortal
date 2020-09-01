using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HealthyGamerPortal.Common.ViewModels.Calendar
{
    public class CalendarViewModel
    {
        public ulong guildId { get; set; }

        public List<SelectListItem> guildList { get; set; }

        public List<DiscordMember> discordMembers { get; set; }
    }
}
