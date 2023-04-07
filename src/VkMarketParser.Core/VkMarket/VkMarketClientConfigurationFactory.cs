using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using VkMarketParser.Core.VkMarket;

namespace VkMarketParser.Core;

public static class VkMarketClientConfigurationFactory
{
    public static IVkMarketClientConfiguration Create(IServiceProvider services)
    {
        var config = services.GetRequiredService<IConfiguration>();
        var vkConfig = config.GetSection("VkMarketClientConfiguration").Get<VkMarketClientConfiguration>();
        if (vkConfig is null) throw new InvalidCastException();
        return vkConfig;
    }
}