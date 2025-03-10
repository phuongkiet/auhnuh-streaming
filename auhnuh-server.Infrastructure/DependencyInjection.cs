using auhnuh_server.Common.Attibutes;
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
    }
}