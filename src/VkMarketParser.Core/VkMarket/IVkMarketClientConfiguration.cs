namespace VkMarketParser.Core.VkMarket;

public interface IVkMarketClientConfiguration
{
    ulong AppId { get; set; }
    string Login { get; set; }
    string Password { get; set; }
    string AccessToken { get; set; }
}