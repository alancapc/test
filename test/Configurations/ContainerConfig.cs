using Microsoft.Extensions.Options;

namespace test.Configurations
{
    using System.IO;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using AutofacSerilogIntegration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

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
            var serviceProvider = services.BuildServiceProvider();
            var serilogConfig = serviceProvider.GetService<IOptions<SerilogConfig>>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(serilogConfig.Value.LoggerFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterLogger(Log.Logger);
            // add other registrations here...

            return builder.Build();
        }
    }
}