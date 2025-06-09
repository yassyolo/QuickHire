namespace QuickHire.Application.Users.Models.Messaging;

public class GetAllMessagesItemModel
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string SenderUsername { get; set; } = string.Empty;
    public string SenderProfilePictureUrl { get; set; } = string.Empty;
    public bool IsStarred { get; set; }
}
