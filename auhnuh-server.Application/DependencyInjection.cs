using auhnuh_server.Application.Extension;
using auhnuh_server.Common.Attibutes;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var autoRegisterableTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.GetCustomAttributes<AutoRegisterAttribute>().Any())
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

            // AutoMapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddAllProfiles();
            });

            services.AddSingleton(autoMapperConfig.CreateMapper());

            return services;
        }
    }
}
