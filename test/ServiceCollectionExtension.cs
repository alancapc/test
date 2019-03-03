using Microsoft.Extensions.DependencyInjection;

namespace test
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            return services;
        }
    }
}