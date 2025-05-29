namespace QuickHire.Infrastructure.Options;

public class SendGridOptions
{
    public const string SendGridOptionsKey = "SendGridOptions";

    public string ApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}
