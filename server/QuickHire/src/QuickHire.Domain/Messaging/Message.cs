using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Messaging;

public class Message : BaseSoftDeletableEntity<int>
{
    public string SenderId { get; set; } = string.Empty;
    public string SenderRole { get; set; } = string.Empty;
    public string ReceiverId { get; set; } = string.Empty;
    public string ReceiverRole { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;
    public string? AttachmentUrl { get; set; } = null!;
    public string? PayloadJson { get; set; }
}
