

using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyGamerPortal.Data.Configuration
{
    public class LogEntryConfiguration : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder.HasKey(le => le.Id);

            builder.ToTable("NLog");
            builder.Property(au => au.Id).HasDefaultValueSql("NEWID()");
        }
    }
}