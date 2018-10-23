using Examples.Json;

namespace Examples
{
    using Microsoft.Extensions.DependencyInjection;
    using Threads;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddExamplesConnector(this IServiceCollection services)
        {
            services.AddTransient<IThreading, Threading>();
            services.AddTransient<IBankAccount, BankAccount>();
            services.AddTransient<IJson, Json.Json>();
            return services;
        }
    }
}
