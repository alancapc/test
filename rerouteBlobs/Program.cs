using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace rerouteBlobs
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Run app
            serviceProvider.GetService().Run();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            // Add logging
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
            serviceCollection.AddLogging();

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Get table storage connection string for serilog
            CloudStorageAccount logStorageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("LoggingStorage"));

            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.AzureTableStorage(logStorageAccount)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton(configuration);

            // Add services
            serviceCollection.AddTransient<IBackupService, BackupService>();

            // Add app
            serviceCollection.AddTransient();
        }

    }
}
