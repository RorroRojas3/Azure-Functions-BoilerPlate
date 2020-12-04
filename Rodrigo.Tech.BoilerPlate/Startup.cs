using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Rodrigo.Tech.BoilerPlate.Extensions.ServiceCollection;
using Rodrigo.Tech.Model.AutoMapper;
using Rodrigo.Tech.Services.Helpers;
using System;

[assembly: FunctionsStartup(typeof(Rodrigo.Tech.BoilerPlate.Startup))]
namespace Rodrigo.Tech.BoilerPlate
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var jsonFile = currentEnvironment.Equals("local", StringComparison.OrdinalIgnoreCase) 
                                    ? "appsettings.local.json" : "appsettings.json";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(DirectoryHelper.GetCurrentDirectory())
                .AddJsonFile(jsonFile, false, true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddLoggingServiceCollection(configuration);
            builder.Services.AddDatabaseServiceCollection();
            builder.Services.AddServicesServiceCollection();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
