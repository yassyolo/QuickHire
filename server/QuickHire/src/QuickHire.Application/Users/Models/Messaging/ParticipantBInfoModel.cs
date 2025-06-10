namespace QuickHire.Application.Users.Models.Messaging;

public class ParticipantBInfoModel
{
    public string Id { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string[] Languages { get; set; } = Array.Empty<string>();
    public string MemberSince { get; set; } = string.Empty;
}
