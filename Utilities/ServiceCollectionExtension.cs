using Microsoft.Extensions.DependencyInjection;

namespace Utilities
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUtilitiesConnector(this IServiceCollection services)
        {
            services.AddTransient<IUtility, Utility>();
            return services;
        }
    }
}