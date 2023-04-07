using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VkMarketParser.Core;

namespace VkMarketParser;

public class Startup
{
    public static async Task Main() =>
        await CreateBuilder().Build().Services.GetRequiredService<Program>().RunAsync();

    private static IHostBuilder CreateBuilder() => Host
        .CreateDefaultBuilder()
        .ConfigureServices(s => s.AddCore().AddUi());
}