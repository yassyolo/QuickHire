using QuickHire.Infrastructure.Persistence.Identity;

namespace QuickHire.Infrastructure.Authentication.Processors;

internal interface IAuthTokenProcessor
{
    (string jwtToken, DateTime expirationTime) GenerateToken(ApplicationUser user, IList<string> roles, string mode);
    string GenerateRefreshToken();
    void WriteTokeToCookie(string cookieName, string token, DateTime expiresAt);
}
