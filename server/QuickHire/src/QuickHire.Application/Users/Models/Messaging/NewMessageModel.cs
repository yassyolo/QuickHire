using Microsoft.AspNetCore.Http;

namespace QuickHire.Application.Users.Models.Messaging;
public class NewMessageModel
{
    public string SenderId { get; set; } = string.Empty;
    public string SenderRole { get; set; } = string.Empty;
    public string ReceiverId { get; set; } = string.Empty;
    public string ReceiverRole { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int ConversationId { get; set; }
    public string? AttachmentUrl { get; set; } 
    public CustomOfferPayloadModel? Payload { get; set; }
}

