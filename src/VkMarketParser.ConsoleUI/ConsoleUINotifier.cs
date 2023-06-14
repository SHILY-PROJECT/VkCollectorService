using VkMarketParser.Core.Notifications;

namespace VkMarketParser;

public class ConsoleUINotifier : INotifier
{
    public void Notify(string message) => Console.WriteLine(message);
}