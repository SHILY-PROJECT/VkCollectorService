namespace VkMarketParser.Core.VkMarket;

public interface IVkMarketClient
{
    string CurrentAccount { get; }
    Task AuthorizeAsync();
    Task<List<Product>> GetProductsAsync(string groupNameOrId, int maxCount);
    Task<ProductResult> GetProductsAsync(string groupNameOrId, int maxCount, bool saveToExcel);
    void DestroyAccessToken();
}