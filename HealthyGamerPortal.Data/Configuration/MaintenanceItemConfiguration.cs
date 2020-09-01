

using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyGamerPortal.Data.Configuration
{
    public class MaintenanceItemConfiguration : IEntityTypeConfiguration<MaintenanceItem>
    {
        public void Configure(EntityTypeBuilder<MaintenanceItem> builder)
        {
            builder.HasKey(oa => oa.Id);

            builder.Property(oa => oa.Name).IsRequired().HasMaxLength(255);
            builder.Property(au => au.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
