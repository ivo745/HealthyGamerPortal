using System;

namespace HealthyGamerPortal.Common.Interfaces
{
    public interface IAuditable
    {
        DateTime? DateCreated { get; set; }
        DateTime? DateChanged { get; set; }
        string CreateUser { get; set; }
        string ChangeUser { get; set; }
    }
}
