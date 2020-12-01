using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Rodrigo.Tech.BoilerPlate.Extensions.ServiceCollection;
using Rodrigo.Tech.Model.AutoMapper;

[assembly: FunctionsStartup(typeof(Rodrigo.Tech.BoilerPlate.Startup))]
namespace Rodrigo.Tech.BoilerPlate
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDatabaseServiceCollection();
            builder.Services.AddServicesServiceCollection();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
