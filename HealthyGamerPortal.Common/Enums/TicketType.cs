
using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.Enums
{
    public enum TicketType
    {
        [Display(Name = "Incident")]
        Incident = 1,
        [Display(Name = "Question")]
        Question = 2,
        [Display(Name = "Non standard change")]
        NonStandardChange = 3,
    }
}
