namespace QuickHire.Infrastructure.Options;

internal class JwtOptions
{
    internal const string JwtOptionsKey = nameof(JwtOptions);

    internal string Secret { get; set; } = string.Empty!;
    internal string Issuer { get; set; } = string.Empty!;
    internal string Audience { get; set; } = string.Empty!;
    internal string ExpirationTimeInMinutes { get; set; } = string.Empty!;
}
