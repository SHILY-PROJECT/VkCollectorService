using VkNet.Model;

namespace VkMarketParser.Core.VkMarket;

public interface IVkMarketClient
{
    Task AuthorizeAsync();
    Task<List<Market>> GetProductsAsync(string groupLink, int maxCount);
}