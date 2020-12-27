using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Services.Helpers;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Rodrigo.Tech.BoilerPlate.Extensions.ServiceCollection
{
    public static class LoggingServiceCollection
    {
        /// <summary>
        ///     Adds Logging service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddLoggingServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            var currentDirectory = DirectoryHelper.GetCurrentDirectory();
            var logDirectory = Path.Combine(currentDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var logger = new LoggerConfiguration()
                            .WriteTo.Console(LogEventLevel.Information)
                            .WriteTo.ApplicationInsightsTraces(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"), LogEventLevel.Information)
                            .ReadFrom.Configuration(configuration)
                            .CreateLogger();
            services.AddSingleton(logger);
            services.AddLogging(l => l.AddSerilog(logger));

            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console(LogEventLevel.Information)
                        .WriteTo.ApplicationInsightsTraces(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"), LogEventLevel.Information)
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
            services.AddSingleton(Log.Logger);
        }
    }
}
