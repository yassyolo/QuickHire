namespace QuickHire.Infrastructure.Options
{
    internal class GoogleAuthenticationOptions
    {
        internal const string GoogleAuthenticationOptionsKey = "GoogleAuthentication";

        internal string ClientId { get; set; } = string.Empty!;
        internal string ClientSecret { get; set; } = string.Empty!;
    }
}
