

using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyGamerPortal.Data.Configuration
{
    public class FaqConfiguration : IEntityTypeConfiguration<FaqItem>
    {
        public void Configure(EntityTypeBuilder<FaqItem> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Order).IsRequired();

            builder.Property(f => f.Question).IsRequired();

            builder.Property(f => f.Answer).IsRequired();

            builder.Property(au => au.Id).HasDefaultValueSql("NEWID()");

        }
    }
}