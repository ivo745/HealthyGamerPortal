
using HealthyGamerPortal.Common.ViewModels.Api.Base;
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.Users
{
    public class ApplicationUserViewModel : BaseViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is a required field")]
        [MaxLength(250)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [MaxLength(250)]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is a required field")]
        [MaxLength(250)]
        public string LastName { get; set; }

        [Display(Name = "Email Adress")]
        [Required(ErrorMessage = "Email Adress is a required field")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [MaxLength(250)]
        public string Email { get; set; }
    }
}
