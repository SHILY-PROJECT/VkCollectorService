namespace VkMarketParser.Core.VkMarket;

public class VkMarketClientConfiguration : IVkMarketClientConfiguration
{
    public ulong AppId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string AccessToken { get; set; }
}