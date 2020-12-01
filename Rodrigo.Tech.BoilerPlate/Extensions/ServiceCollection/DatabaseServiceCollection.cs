using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Repository.Context;
using Rodrigo.Tech.Repository.Pattern.Implementation;
using Rodrigo.Tech.Repository.Pattern.Interface;
using System;

namespace Rodrigo.Tech.BoilerPlate.Extensions.ServiceCollection
{
    public static class DatabaseServiceCollection
    {
        public static void AddDatabaseServiceCollection(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("AZURE_DB")));
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("AZURE_DB"));

            using (var context = new DatabaseContext(optionsBuilder.Options))
                context.Database.Migrate();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
