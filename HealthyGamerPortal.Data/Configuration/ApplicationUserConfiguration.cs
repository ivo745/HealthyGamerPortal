using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyGamerPortal.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(au => au.Id);
            builder.Property(c => c.Email).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Password).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Salt).HasMaxLength(96).IsFixedLength();
            builder.Property(c => c.FirstName).HasMaxLength(255).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(255).IsRequired();
            builder.Property(c => c.MiddleName).HasMaxLength(255);
            builder.Property(au => au.Id).HasDefaultValueSql("NEWID()");
            builder.Property(au => au.Status).IsRequired();

            //builder.Property(i => i.Id).IsRequired();
            //builder.Property(p => p.Password).IsRequired().HasDefaultValueSql("GETDATE()");
            //builder.Property(un => un.UserName).IsRequired().HasMaxLength(10);
        }
    }
}
