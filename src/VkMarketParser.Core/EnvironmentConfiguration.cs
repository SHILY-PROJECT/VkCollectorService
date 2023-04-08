using Microsoft.Extensions.Configuration;

namespace VkMarketParser.Core;

public class EnvironmentConfiguration
{
    public EnvironmentConfiguration(IConfiguration configuration, string currentDirectory, string settingsFileName)
    {
        Configuration = configuration;
        CurrentDirectory = currentDirectory;
        SettingsFileName = settingsFileName;
        SettingsLastVersionFileName = Path.Combine(CurrentDirectory, $"{Path.GetFileNameWithoutExtension(SettingsFileName)}.last_ver{Path.GetExtension(SettingsFileName)}");
    }

    public string CurrentDirectory { get; }
    public string SettingsFileName { get; }
    public string SettingsLastVersionFileName { get; }
    public IConfiguration Configuration { get; }
}