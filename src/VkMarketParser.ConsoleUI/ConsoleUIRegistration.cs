using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VkMarketParser.Core;
using VkMarketParser.Core.Notifications;

namespace VkMarketParser;

public static class ConsoleUIRegistration
{
    private const string SettingsFileName = "appsettings.json";
    
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services
            .AddSingleton(ConfigurationFactory)
            .AddSingleton(EnvironmentConfigurationFactory)
            .AddSingleton(VkMarketClientConfigurationFactory.Create)
            .AddSingleton<Program>()
            .AddSingleton<INotifier, ConsoleUINotifier>();
        
        return services;
    }
    
    private static IConfiguration ConfigurationFactory() => new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(SettingsFileName, true, true)
        .AddUserSecrets<Startup>()
        .Build();

    private static EnvironmentConfiguration EnvironmentConfigurationFactory(IServiceProvider services)
    {
        var config = services.GetRequiredService<IConfiguration>();
        var environmentConfig = new EnvironmentConfiguration(config, Directory.GetCurrentDirectory(), SettingsFileName);
        return environmentConfig;
    }
}