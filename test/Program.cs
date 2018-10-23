namespace test
{
    using Configurations;
    using Interfaces;
    using Autofac;
    using Utilities;
    using Examples;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        private static void Main()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            var container = ContainerConfig.Configure(services);
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }

        public static void ConfigureServices(IServiceCollection services )
        {
            services.AddAutofac();
            services.AddInternalServices();
            services.AddUtilitiesConnector();
            services.AddExamplesConnector();
        }
    }
}
