using VkMarketParser;

var groupLink = "";
var config = new VkMarketClientConfiguration
{
    AppId = 0,
    Login = "",
    Password = ""
};
var client = new VkMarketClient(config);
await client.AuthorizeAsync();
var products = await client.GetProductsAsync(groupLink, 500);

Console.ReadKey();