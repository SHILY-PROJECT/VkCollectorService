using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using VkMarketParser.Core.Notifications;
using VkMarketParser.Core.ResultWriter;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkMarketParser.Core.VkMarket;

public class VkMarketClient : IVkMarketClient
{
    private readonly IVkMarketClientConfiguration _vkServiceConfiguration;
    private readonly VkApi _vk;
    private readonly IMapper _mapper;
    private readonly EnvironmentConfiguration _env;
    private readonly IResultWriter<Product> _resultWriter;
    private readonly INotifier _notifier;
    
    public VkMarketClient(
        IVkMarketClientConfiguration vkServiceConfiguration,
        VkApi vkApi,
        IMapper mapper,
        EnvironmentConfiguration configuration,
        IResultWriter<Product> resultWriter,
        INotifier notifier)
    {
        _vkServiceConfiguration = vkServiceConfiguration;
        _vk = vkApi;
        _mapper = mapper;
        _env = configuration;
        _resultWriter = resultWriter;
        _notifier = notifier;
    }

    public string CurrentAccount => _vkServiceConfiguration.Login;

    public async Task AuthorizeAsync()
    {
        var task = _vkServiceConfiguration.AccessToken switch
        {
            var t when string.IsNullOrWhiteSpace(t) => this.AuthUseLoginAndPasswordAsync(),
            _ => AuthUseAccessTokenAsync()
        };
        await task;
    }
    
    public async Task<List<Product>> GetProductsAsync(string groupNameOrId, int maxCount)
    {
        var group = (await _vk.Groups.GetByIdAsync(null, groupNameOrId, GroupsFields.All)).First();
        
        var offset = 0;
        var maxNumberOfItemsAtTime = 200 > maxCount ? maxCount : 200;
        var allItems = new List<Product>();
        
        while (true)
        {
            var items = await _vk.Markets.GetAsync(-group.Id, null, maxNumberOfItemsAtTime, offset, true);
            allItems.AddRange(_mapper.Map<List<Product>>(items));

            _notifier.Notify($"Прогресс: {allItems.Count} из {((ulong)maxCount > items.TotalCount ? items.TotalCount : maxCount)}");
            
            if ((ulong)allItems.Count >= items.TotalCount || allItems.Count >= maxCount) break;
            
            offset += items.Count;
        }
        
        return allItems;
    }
    
    public async Task<ProductResult> GetProductsAsync(string groupNameOrId, int maxCount, bool saveToExcel)
    {
        var products = await this.GetProductsAsync(groupNameOrId, maxCount);
        
        if (!saveToExcel) return new ProductResult { Products = products };
        
        var fullName = Path.Combine(_env.CurrentDirectory, "result", $"{groupNameOrId}   result   {DateTime.Now:yyyy-MM-dd   HH-mm-ss---fffffff}.xlsx");
        await _resultWriter.WriteAsync(products, fullName);
        
        return new ProductResult { FillNameResult = fullName, Products = products };
    }

    public void DestroyAccessToken() => _vkServiceConfiguration.AccessToken = string.Empty;

    private async Task AuthUseLoginAndPasswordAsync()
    {
        await _vk.AuthorizeAsync(new ApiAuthParams
        {
            ApplicationId = _vkServiceConfiguration.AppId,
            Login = _vkServiceConfiguration.Login,
            Password = _vkServiceConfiguration.Password,
            Settings = Settings.All,
        });
        this.SaveOrRefreshAccessToken();
    }
    
    private async Task AuthUseAccessTokenAsync()
    {
        await _vk.AuthorizeAsync(new ApiAuthParams { AccessToken = _vkServiceConfiguration.AccessToken });
        this.SaveOrRefreshAccessToken();
    }
    
    private void SaveOrRefreshAccessToken()
    {
        _vkServiceConfiguration.AccessToken = _vk.Token;
        File.WriteAllText(_env.SettingsLastVersionFileName, JsonConvert.SerializeObject(_vkServiceConfiguration, Formatting.Indented), Encoding.UTF8);
    }
}