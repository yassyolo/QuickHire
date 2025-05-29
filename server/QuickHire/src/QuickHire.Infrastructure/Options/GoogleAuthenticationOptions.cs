namespace QuickHire.Infrastructure.Options;

public class GoogleAuthenticationOptions
{
    public const string GoogleAuthenticationOptionsKey = "GoogleAuthenticationOptions";

    public string ClientId { get; set; } = string.Empty!;
    public string ClientSecret { get; set; } = string.Empty!;
}
