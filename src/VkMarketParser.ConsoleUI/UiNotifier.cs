using VkMarketParser.Core.Notifications;

namespace VkMarketParser;

public class UiNotifier : INotifier
{
    public void Notify(string message) => Console.WriteLine(message);
}