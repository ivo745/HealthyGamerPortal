using HealthyGamerPortal.Common.Interfaces;
using System;

namespace HealthyGamerPortal.Models
{
    public class FaqItem : IAuditable
    {
        public Guid Id { get; set; }
        public long Order { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        // IAuditable
        public string ChangeUser { get; set; }
        public string CreateUser { get; set; }
        public DateTime? DateChanged { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}