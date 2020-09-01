

using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyGamerPortal.Data.Configuration
{
    public class MailConfiguration : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.MailType).IsRequired();
            builder.Property(m => m.ApplicationUserId).IsRequired();
            builder.Property(m => m.RequesterId).IsRequired();
            builder.Property(m => m.Status).IsRequired();
            builder.Property(au => au.Id).HasDefaultValueSql("NEWID()");

        }
    }
}