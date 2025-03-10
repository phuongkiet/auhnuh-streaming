using Microsoft.Extensions.DependencyInjection;

namespace auhnuh_server.Common.Attibutes
{
    public class AutoRegisterAsConcreteClassAttribute : AutoRegisterAttribute
    {
        public AutoRegisterAsConcreteClassAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) : base(serviceLifetime)
        {
        }
    }
}
