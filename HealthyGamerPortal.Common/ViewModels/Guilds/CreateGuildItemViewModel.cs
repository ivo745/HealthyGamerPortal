using HealthyGamerPortal.Common.ViewModels.Api.Base;
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.Guild
{
    public class CreateGuildItemViewModel : BaseViewModel
    {
        [Display(Name = "Guild Name")]
        [Required(ErrorMessage = "Guild Name is a required field")]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}

