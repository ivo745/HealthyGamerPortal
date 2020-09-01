using System;
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.Guild
{
    public class GuildItemViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Article Title")]
        public string Name { get; set; }

        [Display(Name = "Article Item")]
        public string Item { get; set; }

        public string Icon { get; set; }

        [Display(Name = "Article Date")]
        public DateTime? DateCreated { get; set; }

        public string DateCreatedFormatted { get { return DateCreated.Value.ToString("MMM"); } }
    }
}
