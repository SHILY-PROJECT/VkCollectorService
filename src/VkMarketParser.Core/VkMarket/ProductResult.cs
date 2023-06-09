namespace VkMarketParser.Core.VkMarket;

public record ProductResult
{
    public string FillNameResult { get; init; }
    public List<Product> Products { get; init; }
}