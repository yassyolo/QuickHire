namespace QuickHire.Application.Users.Models.Authentication;

public class ApplicationUserModel
{
    public string Id { get; set; } = string.Empty;
    public string? Email { get; set; } 
    public string? UserName { get; set; } 
    public bool EmailConfirmed { get; set; } 
    public DateTime? RefreshTokenExpirationDate { get; set; }
}
