using Microsoft.Extensions.DependencyInjection;
using Examples.Threads;

namespace Examples
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddExamplesConnector(this IServiceCollection services)
        {
            services.AddTransient<IThreading, Threading>();
            services.AddTransient<IBankAccount, BankAccount>();
            services.AddTransient<IJson, Json>();
            services.AddTransient<ICSharpEight, CSharpEight>();
            return services;
        }
    }
}
