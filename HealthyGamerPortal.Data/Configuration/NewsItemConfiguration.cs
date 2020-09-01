using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyGamerPortal.Data.Configuration
{
    public class NewsItemConfiguration : IEntityTypeConfiguration<NewsItem>
    {
        public void Configure(EntityTypeBuilder<NewsItem> builder)
        {
            builder.HasKey(ni => ni.Id);

            builder.Property(ni => ni.Name).HasMaxLength(255).IsRequired();

            builder.Property(ni => ni.Item).IsRequired();
            builder.Property(au => au.Id).HasDefaultValueSql("NEWID()");
        }
    }
}