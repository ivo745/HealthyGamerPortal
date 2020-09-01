using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.Guild
{
    public class DeleteGuildItemViewModel
    {

        [Display(Name = "Article Title")]
        [Required(ErrorMessage = "Article Title is a required field")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Article Item")]
        public ulong Id { get; set; }

    }
}

