using VkNet.Model;

namespace VkMarketParser.Core.VkMarket;

public interface IVkMarketClient
{
    Task AuthorizeAsync();
    Task<List<Product>> GetProductsAsync(string groupNameOrId, int maxCount);
    Task<ProductResult> GetProductsAsync(string groupNameOrId, int maxCount, bool saveToExcel);
}