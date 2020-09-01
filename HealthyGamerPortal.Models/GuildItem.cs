using System;

namespace HealthyGamerPortal.Models
{
    public class GuildItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // IAuditable
        public string ChangeUser { get; set; }
        public string CreateUser { get; set; }
        public DateTime? DateChanged { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
