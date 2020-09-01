
using HealthyGamerPortal.Common.ViewModels.Api.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.Company
{
    public class CompanyBuildingViewModel : BaseViewModel
    {
        [Display(Name = "Company name")]
        public string Name { get; set; }

        [Display(Name = "Contact name")]
        public string ContactName { get; set; }

        [Display(Name = "Contact emailadress")]
        public string ContactEmail { get; set; }

        [Display(Name = "Building names")]
        public IEnumerable<string> BuildingNames { get; set; }

        [Display(Name = "Buildings")]
        public string Buildings
        {
            get
            {
                return string.Join(",", BuildingNames);
            }
        }
    }
}
