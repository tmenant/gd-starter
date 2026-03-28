using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Godot;


public class Localization
{
    private static readonly Logging.Logger logger = Logging.CreateLogger<Localization>();

    public enum Language
    {
        EN,
        FR,
        DE,
        RU,
    }

    private const string directory = "res://config/localization";

    public static Language language { get; set; } = Language.EN;

    public static Dictionary<Language, Dictionary<string, string>> localizations = new();

    public static void Initialize()
    {
        Initialize(ProjectSettings.GlobalizePath(directory));
    }

    public static void Initialize(string directory)
    {
        localizations[Language.EN] = Read($"{directory}/localization.en.json");
        localizations[Language.FR] = Read($"{directory}/localization.fr.json");
        localizations[Language.DE] = Read($"{directory}/localization.de.json");
        localizations[Language.RU] = Read($"{directory}/localization.ru.json");

        logger.Info($"Initialized {localizations.Count} localization files.");
    }

    private static Dictionary<string, string> Read(string path)
    {
        if (!File.Exists(path))
        {
            return new Dictionary<string, string>();
        }

        using (var stream = File.OpenRead(path))
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(stream);
        }
    }

    public static string Get(string key)
    {
        if (localizations.TryGetValue(language, out var dict) && dict.TryGetValue(key, out var result))
        {
            return result;
        }

        return key;
    }

    public static string Get(string key, params object[] args)
    {
        return "";
    }
}
