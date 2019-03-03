using Microsoft.Extensions.DependencyInjection;
using Utilities.Implementations;
using Utilities.Interfaces;

namespace Utilities
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUtilitiesConnector(this IServiceCollection services)
        {
            services.AddTransient<IUtility, Utility>();
            services.AddTransient<IInitialiseLookups, InitialiseLookups>();
            return services;
        }
    }
}