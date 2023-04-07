using VkMarketParser.Core.VkMarket;

namespace VkMarketParser;

public class Program
{
    private readonly IVkMarketClient _client;
    
    public Program(IVkMarketClient vkMarketClient)
    {
        _client = vkMarketClient;
    }
    
    public async Task RunAsync()
    {
        var groupLink = "";
        
        await _client.AuthorizeAsync();
        var products = await _client.GetProductsAsync(groupLink, 500);
    }
}