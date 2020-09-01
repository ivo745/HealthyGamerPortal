using HealthyGamerPortal.Common.ViewModels.Guilds;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.Guild
{
    public class EditGuildItemViewModel
    {
        [Display(Name = "Id")]
        public ulong Id { get; set; }

        [Display(Name = "Guild Name")]
        [Required(ErrorMessage = "Guild Name is a required field")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Invite User")]
        public List<GuildUserViewModel> guildUsers;
    }
}
