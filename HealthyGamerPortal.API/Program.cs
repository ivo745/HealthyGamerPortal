using HealthyGamerPortal.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace HealthyGamerPortal.API
{
    /// <summary>
    /// The main entry point to the application.
    /// </summary>
    public class Program
    {
        private static readonly NLog.Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            try
            {
                logger.Debug("init main function");

                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<HealthyGamerPortalDbContext>();
                SeedData.Initialize(services);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in init");
                host.Dispose();
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
            host.Run();
        }

        /// <summary>
        /// Creates the web host builder for the kestral server.
        /// </summary>
        /// <returns>The web host builder.</returns>
        /// <param name="args">Arguments.</param>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();  // NLog: setup NLog for Dependency injection
    }
}