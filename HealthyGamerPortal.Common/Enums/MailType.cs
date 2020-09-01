using System.ComponentModel.DataAnnotations;

namespace HealthyGamerPortal.Common.Enums
{
    public enum MailType
    {
        [Display(Name = "Account created")]
        AccCreated,
        [Display(Name = "New port allocation approval")]
        NewPortAllocationApproval,
        [Display(Name = "New port deletion approval")]
        NewPortDeletionApproval,
        [Display(Name = "Password reset")]
        PassReset,
        [Display(Name = "New subscription")]
        NewSub,
        [Display(Name = "New Incident")]
        NewIncMail,
        [Display(Name = "New Question")]
        NewQueMail,
        [Display(Name = "New Non Standard Change")]
        NewNonMail,
        [Display(Name = "New Port Patch")]
        NewPortPatch,
        [Display(Name = "Unknown")]
        Unknown,
    }
}
