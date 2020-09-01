using HealthyGamerPortal.Common.ViewModels.Api.Base;
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.ViewModels.LocalizationRecords
{
    public class LocalizationRecordViewModel : BaseViewModel
    {
        [Display(Name = "Key")]
        public string Key { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Culture")]
        public string LocalizationCulture { get; set; }

    }
}

