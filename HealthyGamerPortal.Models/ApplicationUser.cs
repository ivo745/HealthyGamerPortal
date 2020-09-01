using HealthyGamerPortal.Common.Enums;
using HealthyGamerPortal.Common.Interfaces;
using System;

namespace HealthyGamerPortal.Models
{
    public class ApplicationUser : IAuditable
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime? PasswordResetExpireDate { get; set; }
        public string PasswordResetHash { get; set; }
        public DateTime? LastLoggedOn { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public UserStatus Status { get; set; }

        // IAuditable
        public string ChangeUser { get; set; }
        public string CreateUser { get; set; }
        public DateTime? DateChanged { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}