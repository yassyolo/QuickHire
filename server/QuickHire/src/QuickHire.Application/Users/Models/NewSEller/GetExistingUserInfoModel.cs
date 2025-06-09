using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Models.NewSEller;
public class GetExistingUserInfoModel
{
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public List<UserLanguageModel> Languages { get; set; } = new List<UserLanguageModel>();
}
