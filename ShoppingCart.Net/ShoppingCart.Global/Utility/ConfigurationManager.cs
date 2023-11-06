using System.Reflection;
using System.Text.Json;
using ShoppingCart.Global.Utility.Settings;

namespace ShoppingCart.Global.Utility;

public static class ConfigurationManager
{
    private static ApplicationSettings _applicationSettings;
    
    static ConfigurationManager()
    {
        LoadSettings();
    }

    public static ApplicationSettings ApplicationSettings => _applicationSettings;

    private static void LoadSettings()
    {
        var assembly = Assembly.GetEntryAssembly();
        var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
        var appsettingsPath = Path.Combine(assemblyDirectory, "appsettings.json");
        var path = appsettingsPath;

        if (!File.Exists(path)) 
            throw new Exception("appsettings.json not found.");
        
        var settings = File.ReadAllText(path);
        _applicationSettings = JsonSerializer.Deserialize<ApplicationSettings>(settings);

    }
}