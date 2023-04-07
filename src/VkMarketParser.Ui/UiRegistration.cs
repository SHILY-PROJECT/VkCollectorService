using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VkMarketParser.Core.VkMarket;

namespace VkMarketParser;

public static class UiRegistration
{
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services
            .AddSingleton(ConfigurationFactory)
            .AddSingleton(VkMarketClientConfigurationFactory)
            .AddSingleton<Program>();
        
        return services;
    }
    
    private static IConfiguration ConfigurationFactory() => new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .AddUserSecrets<Startup>()
        .Build();

    private static IVkMarketClientConfiguration VkMarketClientConfigurationFactory(IServiceProvider services)
    {
        var config = services.GetRequiredService<IConfiguration>();
        var vkConfig = config.GetSection("VkMarketClientConfiguration").Get<VkMarketClientConfiguration>();
        if (vkConfig is null) throw new InvalidCastException();
        return vkConfig;
    }
}