using HealthyGamerPortal.API.Interfaces;
using HealthyGamerPortal.API.Services;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HealthyGamerPortal.API.Helpers
{
    /// <summary>
    /// Class that registers all custom services or services that need to be injectable with the AspNetCore dependency injection mechanism.
    /// </summary>
    public static class ServicesConfig
    {
        /// <summary>
        /// Register base services that do not require a specific implementation.
        /// </summary>
        /// <param name="services">The services that are registered to the API.</param>
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            services.AddSingleton(typeof(IAuthentication), typeof(AuthenticationService));
            services.AddSingleton(typeof(ITokenService), typeof(JwtTokenService));
            services.AddScoped(typeof(ICompanyService), typeof(CompanyService));
            services.AddScoped(typeof(IApplicationUserService), typeof(ApplicationUserService));
            services.AddScoped(typeof(INewsService), typeof(NewsService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(ILocalizationRecordService), typeof(LocalizationRecordService));
        }

        /// <summary>
        /// Register a specific service with an actual implementation.
        /// These will be registered as a singleton.
        /// </summary>
        /// <typeparam name="T">The type of the service that will be registered.</typeparam>
        /// <param name="services">The services that are registered to the API.</param>
        /// <param name="implementation">The actual instance of <typeparamref name="T"/> that will be injected.</param>
        public static void RegisterCustomSingletonImplementation<T>(IServiceCollection services, T implementation) where T : class
        {
            services.AddSingleton(implementation);
        }
    }
}