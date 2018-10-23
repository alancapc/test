using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Utilities.Model;

namespace Utilities
{
    using Microsoft.Extensions.DependencyInjection;
    using Implementations;
    using Interfaces;
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