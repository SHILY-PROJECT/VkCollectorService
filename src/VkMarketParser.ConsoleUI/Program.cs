using VkMarketParser.Core.VkMarket;
using VkNet.Exception;

namespace VkMarketParser;

public class Program
{
    private readonly IVkMarketClient _client;
    
    public Program(IVkMarketClient vkMarketClient)
    {
        _client = vkMarketClient;
    }
    
    public async Task RunAsync()
    {
        int maxCount;
        string groupLinkOrNameOrId;
        
        while (true)
        {
            Console.WriteLine("Введите ссылку на группу: ");
            groupLinkOrNameOrId = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(groupLinkOrNameOrId)) continue;
            
            Console.Clear();
            break;
        }
        while (true)
        {
            Console.WriteLine("Введите макисмальное количество товаров которое нужно спарсить: ");
            
            if (!int.TryParse(Console.ReadLine(), out maxCount) && maxCount <= 0) continue;
            
            Console.Clear();
            break;
        }

        var group = groupLinkOrNameOrId.Contains("http") ? Path.GetFileName(groupLinkOrNameOrId) : groupLinkOrNameOrId;

        while (true)
        {
            try
            {
                Console.WriteLine($"Авторизация аккаунта '{_client.CurrentAccount}'...");
                await _client.AuthorizeAsync();
                Console.WriteLine($"Авторизация аккаунта '{_client.CurrentAccount}' прошла успешно!");

                Console.WriteLine($"Запуск парсинга товаров группы '{groupLinkOrNameOrId}'...");
                var productsResult = await _client.GetProductsAsync(group, maxCount, true);
                Console.WriteLine($"Парсинг группы '{groupLinkOrNameOrId}' успешно завершен!");
                Console.WriteLine($"Файл с результатом: {productsResult.FillNameResult}");
            }
            catch (UserAuthorizationFailException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                _client.DestroyAccessToken();
                continue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            break;
        }

        Console.ReadKey();
    }
}