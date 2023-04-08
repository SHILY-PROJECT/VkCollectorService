namespace VkMarketParser.Core.ResultWriter;

public interface IResultWriter<in TItem>
{
    Task WriteAsync(IEnumerable<TItem> products, string fullFileName);
}