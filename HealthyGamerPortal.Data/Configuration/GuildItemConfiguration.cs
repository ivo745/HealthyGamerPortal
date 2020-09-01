using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyGamerPortal.Data.Configuration
{
    public class GuildItemConfiguration : IEntityTypeConfiguration<GuildItem>
    {
        public void Configure(EntityTypeBuilder<GuildItem> builder)
        {
            builder.HasKey(ni => ni.Id);

            builder.Property(ni => ni.Name).HasMaxLength(255).IsRequired();

            builder.Property(au => au.Id).HasDefaultValueSql("NEWID()");
        }
    }
}