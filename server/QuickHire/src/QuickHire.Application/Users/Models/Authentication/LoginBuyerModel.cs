namespace QuickHire.Application.Users.Models.Authentication;

public class LoginBuyerModel
{
    public string EmailOrUsername { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
