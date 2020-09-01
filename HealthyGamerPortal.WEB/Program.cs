using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace HealthyGamerPortal.WEB
{
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
                .UseSetting("https_port", "8080")
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();  // NLog: setup NLog for Dependency injection
    }
}