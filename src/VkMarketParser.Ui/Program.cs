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
        var groupLinkOrNameOrId = "";

        var group = groupLinkOrNameOrId.Contains("http") ? Path.GetFileName(groupLinkOrNameOrId) : groupLinkOrNameOrId;
        await _client.AuthorizeAsync();
        var products = await _client.GetProductsAsync(group, 500);
    }
}