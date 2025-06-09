namespace QuickHire.Application.Users.Models.Profile;
public class BuyerProfileModel
{
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MemberSince { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public UserLanguageModel[] Languages { get; set; } = Array.Empty<UserLanguageModel>();
}
