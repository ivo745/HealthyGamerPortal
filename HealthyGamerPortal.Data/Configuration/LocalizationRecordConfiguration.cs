using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyGamerPortal.Data.Configuration
{
    public class LocalizationRecordConfiguration : IEntityTypeConfiguration<LocalizationRecord>
    {
        public void Configure(EntityTypeBuilder<LocalizationRecord> builder)
        {
            builder.HasKey(au => au.Id);
            builder.Property(c => c.Key).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(255).IsRequired();
            builder.Property(c => c.LocalizationCulture).HasMaxLength(255).IsRequired();
            builder.HasIndex(c => new { c.Key, c.LocalizationCulture }).IsUnique();
            builder.Property(au => au.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
