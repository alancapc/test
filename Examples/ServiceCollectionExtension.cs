using Examples.DesignPatterns.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace Examples
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddExamplesConnector(this IServiceCollection services)
        {
            services.AddSingleton<IPersonFactory, PersonFactory>();
            services.AddSingleton<IFactoryDemo, FactoryDemo>();

            return services;
        }
    }
}