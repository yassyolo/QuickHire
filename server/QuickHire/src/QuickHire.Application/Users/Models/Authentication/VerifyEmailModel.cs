namespace QuickHire.Application.Users.Models.Authentication;

public class VerifyEmailModel
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
