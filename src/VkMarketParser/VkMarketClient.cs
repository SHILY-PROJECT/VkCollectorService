using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkMarketParser;

public class VkMarketClient
{
    private readonly VkMarketClientConfiguration _configuration;
    private readonly VkApi _vk;
    
    public VkMarketClient(VkMarketClientConfiguration configuration)
    {
        _configuration = configuration;
        _vk = new VkApi();
    }

    public async Task AuthorizeAsync()
    {
        var task = _configuration.AccessToken switch
        {
            var t when string.IsNullOrWhiteSpace(t) => this.AuthUseLoginAndPasswordAsync(),
            _ => AuthUseAccessTokenAsync()
        };
        await task;
    }

    public async Task<List<Market>> GetProductsAsync(string groupLink, int maxCount)
    {
        var group = (await _vk.Groups.GetByIdAsync(null, groupLink, GroupsFields.All)).First();
        
        var offset = 0;
        var allItems = new List<Market>();
        
        while (true)
        {
            var items = await _vk.Markets.GetAsync(-group.Id, null, 200, offset, true);
            allItems.AddRange(items);
            
            if ((ulong)allItems.Count >= items.TotalCount || allItems.Count >= maxCount) break;
            
            offset += items.Count;
        }
        
        return allItems; 
    }

    private async Task AuthUseLoginAndPasswordAsync()
    {
        await _vk.AuthorizeAsync(new ApiAuthParams
        {
            ApplicationId = _configuration.AppId,
            Login = _configuration.Login,
            Password = _configuration.Password,
            Settings = Settings.All,
        });
        _configuration.AccessToken = _vk.Token;
    }
    
    private async Task AuthUseAccessTokenAsync()
    {
        await _vk.AuthorizeAsync(new ApiAuthParams
        {
            AccessToken = _configuration.AccessToken
        });
        _configuration.AccessToken = _vk.Token;
    }
}