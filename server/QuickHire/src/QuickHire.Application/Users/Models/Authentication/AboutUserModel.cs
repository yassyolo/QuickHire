namespace QuickHire.Application.Users.Models.Authentication;

public class AboutUserModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
    public string Mode { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
}
