namespace QuickHire.Infrastructure.Options;

internal class CloudinaryOptions
{
    internal const string CloudinaryOptionsKey = "CloudinaryOptions";
    internal string CloudName { get; set; } = string.Empty;
    internal string ApiKey { get; set; } = string.Empty;
    internal string ApiSecret { get; set; } = string.Empty;
}
