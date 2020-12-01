using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;

namespace Rodrigo.Tech.BoilerPlate.Extensions.ServiceCollection
{
    public static class ServiceServiceCollection
    {
        public static void AddServicesServiceCollection(this IServiceCollection services)
        {
            services.AddTransient<IItemService, ItemService>();
        }
    }
}
