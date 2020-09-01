using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.Company
{
    public class CreateCompanyBuildingViewModel
    {
        [Display(Name = "Company name")]
        //[Required(ErrorMessage = "Company name is a required field")]
        //[MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Contact name")]
        //[Required(ErrorMessage = "Contact name is a required field")]
        //[MaxLength(250)]
        public string ContactName { get; set; }

        [Display(Name = "Contact emailadress")]
        //[Required(ErrorMessage = "Contact emailadress is a required field")]
        //[EmailAddress(ErrorMessage = "Please enter a valid email address")]
        //[MaxLength(250)]
        public string ContactEmail { get; set; }
    }
}
