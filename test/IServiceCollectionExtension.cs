﻿using Microsoft.Extensions.DependencyInjection;
using test.Threads;

namespace test
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            services.AddTransient<IThreading, Threading>();
            services.AddTransient<IBankAccount, BankAccount>();
            return services;
        }
    }
}
