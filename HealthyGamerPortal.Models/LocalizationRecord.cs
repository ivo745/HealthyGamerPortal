using HealthyGamerPortal.Common.Interfaces;
using System;

namespace HealthyGamerPortal.Models
{
    public class LocalizationRecord : IAuditable
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public string LocalizationCulture { get; set; }

        // IAuditable
        public string ChangeUser { get; set; }
        public string CreateUser { get; set; }
        public DateTime? DateChanged { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}