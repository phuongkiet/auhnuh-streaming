using AutoMapper;

namespace auhnuh_server.Application.Extension
{
    public static class IMapperConfigurationExpressionExtentions
    {
        public static void AddAllProfiles(this IMapperConfigurationExpression configuration)
        {
            var profiles = typeof(IMapperConfigurationExpressionExtentions).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(Profile)))
                .Select(t => Activator.CreateInstance(t) as Profile);
            configuration.AddProfiles(profiles);
        }
    }
}
