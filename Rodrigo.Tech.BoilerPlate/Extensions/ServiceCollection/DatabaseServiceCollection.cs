using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Model.Constants;
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
            var db = Environment.GetEnvironmentVariable(EnvironmentConstants.AZURE_DB);
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(db));
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(db);

            using (var context = new DatabaseContext(optionsBuilder.Options))
                context.Database.Migrate();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
