using HealthyGamerPortal.Common.Enums;
using HealthyGamerPortal.Common.Extensions;
using HealthyGamerPortal.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HealthyGamerPortal.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<HealthyGamerPortalDbContext>();
            context.Database.EnsureCreated();

            InsertFaqItems();
            InsertApplicationUsers();
            InsertLocalizationRecordItems();

            void InsertFaqItems()
            {
                if (!context.FaqItem.Any())
                {
                    context.FaqItem.Add(new FaqItem { Id = Guid.NewGuid(), Order = 1, Question = "Hoe werkt dit?", Answer = "In stijl :D" });
                }
            }

            void InsertLocalizationRecordItems()
            {
                if (!context.LocalizationRecord.Any())
                {
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "DashboardButtonText", Text = "Dashboard", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "CompanyButtonText", Text = "Company", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "UsersButtonText", Text = "Users", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "GuildsButtonText", Text = "Guilds", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "CalendarButtonText", Text = "Calendar", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "SupportButtonText", Text = "Support", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "FAQButtonText", Text = "FAQ", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "ManagementButtonText", Text = "Management", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "FAQManagementButtonText", Text = "FAQ", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "NewsManagementButtonText", Text = "News", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "ApplicationTextButtonText", Text = "Application Text", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                    context.LocalizationRecord.Add(new LocalizationRecord { Id = Guid.NewGuid(), Key = "AuthorizationManagementButtonText", Text = "Authorization", LocalizationCulture = LocalizationCulture.enUS.GetStringValue() });
                }
            }

            void InsertApplicationUsers()
            {
                if (!context.ApplicationUsers.Any())
                {
                    var theBoss = new ApplicationUser { Id = Guid.NewGuid(), FirstName = "Dev", LastName = "Overlord", Password = "Fm0SeMA7ZPVNZUpUALyFkBP1LtWTmr8k2oS7u/phJe8tGcFmeKUMBH3eTDD1frjrUXVL6CXNSQT4fUXojbOwdQ==", Email = "test@test.test", PasswordResetExpireDate = null, PasswordResetHash = "", LastLoggedOn = null, Status = Common.Enums.UserStatus.Confirmed };
                    context.ApplicationUsers.Add(theBoss);
                }
            }

            context.SaveChanges();
        }
    }
}