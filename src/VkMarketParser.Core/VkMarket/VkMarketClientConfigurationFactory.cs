using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkMarketParser.Core.VkMarket;

namespace VkMarketParser.Core;

public static class VkMarketClientConfigurationFactory
{
    public static IVkMarketClientConfiguration Create(IServiceProvider services)
    {
        var config = services.GetRequiredService<IConfiguration>();
        var envConfig = services.GetRequiredService<EnvironmentConfiguration>();
        var mapper = services.GetRequiredService<IMapper>();
        var vkConfig = config.GetSection(nameof(VkMarketClientConfiguration)).Get<VkMarketClientConfiguration>();
        
        if (vkConfig is null) throw new InvalidCastException();
        
        if (File.Exists(envConfig.SettingsLastVersionFileName) && JsonConvert.DeserializeObject<VkMarketClientConfiguration>(File.ReadAllText(envConfig.SettingsLastVersionFileName, Encoding.UTF8)) is { } vkConfigLastVer)
        {
            mapper.Map(vkConfigLastVer, vkConfig);
        }
        
        return vkConfig;
    }
}