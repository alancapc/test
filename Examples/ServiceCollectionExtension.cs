using Examples.Json;
using Examples.Threads;
using Microsoft.Extensions.DependencyInjection;

namespace Examples
{
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