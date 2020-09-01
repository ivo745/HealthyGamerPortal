using HealthyGamerPortal.Common.Interfaces;
using System;

namespace HealthyGamerPortal.Models
{
    public class Mail : IAuditable
    {
        public Guid Id { get; set; }
        public string MailType { get; set; }
        public string MailTo { get; set; }
        public string Link { get; set; }
        public Guid? ApplicationUserId { get; set; }
        public Guid? RequesterId { get; set; }
        public string TicketTitle { get; set; }
        public string TicketInfo { get; set; }
        public int Status { get; set; }

        // IAuditable
        public string ChangeUser { get; set; }
        public string CreateUser { get; set; }
        public DateTime? DateChanged { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}