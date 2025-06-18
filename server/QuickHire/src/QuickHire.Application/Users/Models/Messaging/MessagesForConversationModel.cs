namespace QuickHire.Application.Users.Models.Messaging;

public class MessagesForConversationModel
{
    public int Id { get; set; }
    public string SenderProfilePictureUrl { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;
    public string SenderUsername { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public object? Payload { get; set; }
    public string? FileUrl { get; set; }
}
