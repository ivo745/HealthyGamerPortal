using HealthyGamerPortal.Common.Interfaces;
using System;

namespace HealthyGamerPortal.Models
{
    public class NewsItem : IAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Item { get; set; }

        // IAuditable
        public string ChangeUser { get; set; }
        public string CreateUser { get; set; }
        public DateTime? DateChanged { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
