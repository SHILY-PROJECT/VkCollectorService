namespace VkMarketParser.Core.VkMarket;

public record ProductResult
{
    public string ResultFillName { get; init; }
    public List<Product> Products { get; init; }
}