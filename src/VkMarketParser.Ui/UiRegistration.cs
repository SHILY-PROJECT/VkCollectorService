using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VkMarketParser.Core;

namespace VkMarketParser;

public static class UiRegistration
{
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services
            .AddSingleton(ConfigurationFactory)
            .AddSingleton(VkMarketClientConfigurationFactory.Create)
            .AddSingleton<Program>();
        
        return services;
    }
    
    private static IConfiguration ConfigurationFactory() => new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .AddUserSecrets<Startup>()
        .Build();
}