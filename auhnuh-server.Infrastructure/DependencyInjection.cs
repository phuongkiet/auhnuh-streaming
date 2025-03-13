using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain;
using auhnuh_server.Infrastructure.Data;
using auhnuh_server.Infrastructure.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace auhnuh_server.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var assemblyTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes());
            var autoRegisterableTypes = assemblyTypes
                .Where(t => t.GetCustomAttributes<AutoRegisterAttribute>().Any())
                .Where(t => t.IsClass && !t.IsAbstract);
            var autoRegisterableAsConcreteClassTypes = assemblyTypes
                .Where(t => t.GetCustomAttributes<AutoRegisterAsConcreteClassAttribute>().Any())
                .Where(t => t.IsClass && !t.IsAbstract);
            foreach (var registerableType in autoRegisterableTypes)
            {
                var interfaceType = typeof(DependencyInjection).Assembly
                    .GetTypes()
                    .Where(t => t.IsInterface && t.IsAssignableFrom(registerableType));
                var attribute = registerableType.GetCustomAttribute<AutoRegisterAttribute>() as AutoRegisterAttribute;
                var lifeTime = attribute?.ServiceLifetime ?? ServiceLifetime.Scoped;
                foreach (var iType in interfaceType)
                    services.Add(new ServiceDescriptor(iType, registerableType, lifeTime));
            }
            foreach (var registerableType in autoRegisterableAsConcreteClassTypes)
            {
                var attribute = registerableType.GetCustomAttribute<AutoRegisterAsConcreteClassAttribute>() as AutoRegisterAsConcreteClassAttribute;
                var lifeTime = attribute?.ServiceLifetime ?? ServiceLifetime.Scoped;
                services.Add(new ServiceDescriptor(registerableType, registerableType, lifeTime));
            }

            return services;
        }
        public static async Task<IApplicationBuilder> SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            await Seed.SeedUser(dbContext, userManager, roleManager);
            await Seed.SeedCategoryFromJson(dbContext);
            await Seed.SeedMovieFromJson(dbContext);

            return app;
        }
    }
}