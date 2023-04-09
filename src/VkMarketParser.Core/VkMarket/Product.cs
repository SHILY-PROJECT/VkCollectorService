namespace VkMarketParser.Core.VkMarket;

public record Product
{
    public string Name { get; init; }
    public string Link { get; init; }
    public string Description { get; init; }
    public List<string> Images { get; init; }
}