namespace QuickHire.Infrastructure.Options;

internal class SendGridOptions
{
    internal const string SendGridOptionsKey = "SendGridOptions";

    internal string ApiKey { get; set; } = string.Empty;
    internal string FromEmail { get; set; } = string.Empty;
    internal string FromName { get; set; } = string.Empty;
}
