namespace VkMarketParser.Core.Notifications;

public interface INotifier
{
    public void Notify(string message);
}