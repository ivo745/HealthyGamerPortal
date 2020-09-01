using HealthyGamerPortal.Common.ViewModels.Api.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.Calendar
{
    public class GuildUserItemViewModel : BaseViewModel
    {

        [Display(Name = "Member Name")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Member Note")]
        [MaxLength(250)]
        public string Note { get; set; }

        [Display(Name = "Note Date")]
        public DateTime? DateCreated { get; set; }
    }
}