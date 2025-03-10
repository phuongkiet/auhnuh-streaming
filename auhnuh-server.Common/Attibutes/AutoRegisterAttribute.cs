using Microsoft.Extensions.DependencyInjection;

namespace auhnuh_server.Common.Attibutes
{
    public class AutoRegisterAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; }

        public AutoRegisterAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            ServiceLifetime = serviceLifetime;
        }
    }
}
