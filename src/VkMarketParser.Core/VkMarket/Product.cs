namespace VkMarketParser.Core.VkMarket;

public record Product
{
    public string Name { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public int Likes { get; set; }
}