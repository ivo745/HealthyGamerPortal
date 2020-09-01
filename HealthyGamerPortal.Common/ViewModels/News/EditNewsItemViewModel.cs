
using HealthyGamerPortal.Common.ViewModels.Api.Base;
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.News
{
    public class EditNewsItemViewModel : BaseViewModel
    {

        [Display(Name = "Article Title")]
        [Required(ErrorMessage = "Article Title is a required field")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Article Item")]
        [Required(ErrorMessage = "Article Item is a required field")]
        [MaxLength(250)]
        public string Item { get; set; }

    }
}

