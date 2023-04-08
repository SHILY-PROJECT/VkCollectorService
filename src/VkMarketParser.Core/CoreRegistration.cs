using Microsoft.Extensions.DependencyInjection;
using VkMarketParser.Core.VkMarket;
using VkNet;

namespace VkMarketParser.Core;

public static class CoreRegistration
{
    public static IServiceCollection AddCore(this IServiceCollection service)
    {
        service
            .AddAutoMapper(typeof(MapperProfile))
            .AddSingleton<IVkMarketClientConfiguration, VkMarketClientConfiguration>()
            .AddScoped<IVkMarketClient, VkMarketClient>()
            .AddScoped<VkApi>(_ => new VkApi());
        
        return service;
    }
}