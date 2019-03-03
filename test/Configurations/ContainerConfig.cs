using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using test.Interfaces;

namespace test.Configurations
{
    public static class ContainerConfig
    {
        public static IContainer Configure(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app-settings.json", false)
                .Build();
            services.AddOptions();
            services.Configure<SerilogConfig>(configuration.GetSection("Serilog"));
            services.Configure<SourceConfig>(configuration.GetSection("Source"));
            var serviceProvider = services.BuildServiceProvider();
            var serilogConfig = serviceProvider.GetService<IOptions<SerilogConfig>>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(serilogConfig.Value.LoggerFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = new ContainerBuilder();

            builder.RegisterLogger(Log.Logger);
            builder.Populate(services);
            builder.RegisterType<Application>().As<IApplication>();
            // add other registrations here...

            return builder.Build();
        }
    }
}