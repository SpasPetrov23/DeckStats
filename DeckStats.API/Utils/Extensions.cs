using System.Text.Json;

namespace DeckStats.API.Utils;

public static class Extensions
{
    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}
