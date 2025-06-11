namespace QuickHire.Infrastructure.Extensions;

internal static class PlaceholderExtension
{
    internal static string ReplacePlaceHolders(string text, Dictionary<string, string> placeholders)
    {
        foreach (var kvp in placeholders)
        {
            text = text.Replace($"{{{kvp.Key}}}", kvp.Value);
        }
        return text;
    }
}
