using HealthyGamerPortal.Common.Interfaces;
using HealthyGamerPortal.Data.Configuration;
using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Data
{
    public class HealthyGamerPortalDbContext : DbContext
    {
        public HealthyGamerPortalDbContext()
        {
            var dbContextFactory = new DbContextFactory();
        }

        public HealthyGamerPortalDbContext(DbContextOptions<HealthyGamerPortalDbContext> options) : base(options) { }

        /// <summary>
        /// All Models (tables) that we need
        /// </summary>
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<FaqItem> FaqItem { get; set; }
        public virtual DbSet<LocalizationRecord> LocalizationRecord { get; set; }
        public virtual DbSet<Mail> Mail { get; set; }
        public virtual DbSet<MaintenanceItem> MaintenanceItem { get; set; }
        public virtual DbSet<NewsItem> NewsItem { get; set; }
        public virtual DbSet<GuildItem> GuildItem { get; set; }
        public virtual DbSet<LogEntry> LogEntries { get; set; }

        /// <summary>
        /// Specific tricks needed on Models
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specify on delete action
            // .OnDelete(DeleteBehavior.Restrict);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new MaintenanceItemConfiguration());
            modelBuilder.ApplyConfiguration(new FaqConfiguration());
            modelBuilder.ApplyConfiguration(new LogEntryConfiguration());
            modelBuilder.ApplyConfiguration(new MailConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizationRecordConfiguration());
            modelBuilder.ApplyConfiguration(new NewsItemConfiguration());
        }

        /// <summary>
        /// Creates a new context.
        /// </summary>
        /// <returns></returns>
        public static HealthyGamerPortalDbContext Create()
        {
            return new HealthyGamerPortalDbContext();
        }

        /// <summary>
        /// Override for SaveChangesAsync to support soft deleting items.
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Audit the entries.
            AuditEntries();

            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Override for SaveChanges to support soft deleting items.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            // Audit the entries.
            AuditEntries();

            return base.SaveChanges();
        }

        private void AuditEntries()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IAuditable && (x.State == EntityState.Added || x.State == EntityState.Modified));

            // TODO: Figure out what the best way is to retrieve the user.
            // The following is based on HTTP and is no guarantee for an Id.

            var currentUsername = string.Empty;

            //Get's users name
            //currentUsername = Thread.CurrentPrincipal.Identity.Name; //LOGIC NEEDS TO BE CHANGED
            if (currentUsername == string.Empty)
            {
                currentUsername = "anonymous";
            }

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IAuditable)entity.Entity).DateCreated = DateTime.Now;
                    ((IAuditable)entity.Entity).CreateUser = currentUsername;
                }

                ((IAuditable)entity.Entity).DateChanged = DateTime.Now;
                ((IAuditable)entity.Entity).ChangeUser = currentUsername;
            }
        }
    }

    public class DbContextFactory : IDesignTimeDbContextFactory<HealthyGamerPortalDbContext>
    {
        public HealthyGamerPortalDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<HealthyGamerPortalDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"), options => options.EnableRetryOnFailure());
            return new HealthyGamerPortalDbContext(builder.Options);
        }
    }
}
