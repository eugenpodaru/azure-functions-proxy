using Devlight.Azure.Functions.Proxy;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Devlight.Azure.Functions.Proxy
{
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = GetConfiguration(builder.Services);

            builder.Services.AddSingleton(configuration);
            builder.Services.AddLogging();

            builder.Services.AddOptions();

            builder.Services.AddHttpClient<IProxyService, ProxyService>();
        }

        private static IConfiguration GetConfiguration(IServiceCollection services)
        {
            IConfigurationBuilder configuration = new ConfigurationBuilder();

            if (services.FirstOrDefault(s => s.ServiceType == typeof(IConfiguration))?.ImplementationInstance is IConfiguration existingConfiguration)
            {
                configuration = configuration.AddConfiguration(existingConfiguration);
            }

            configuration = configuration.SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables();

            return configuration.Build();
        }
    }
}
