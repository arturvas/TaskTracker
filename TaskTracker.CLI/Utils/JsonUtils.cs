using System.Text.Json;

namespace TaskTracker.CLI.Utils;

public static class JsonUtils
{
    public static JsonSerializerOptions GetJsonOptions()
    {
        #if DEBUG
        return new JsonSerializerOptions { WriteIndented = true };
        #else
        return new JsonSerializerOptions { WriteIndented = false };
        #endif
    }
}