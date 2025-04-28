using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.CustomRequests;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Messaging;

public class Message : BaseSoftDeletableEntity<int>
{
    public string SenderId { get; set; } = string.Empty;
    public string ReceiverId { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;
    public string? AttachmentUrl { get; set; } = null!;
    public int? CustomOfferId { get; set; } 
    public CustomOffer? CustomOffer { get; set; } 
    public int? CustomRequestId { get; set; }
    public CustomRequest? CustomRequest { get; set; }
    public int? DeliveryId { get; set; }
    public Delivery? Delivery { get; set; }
    public int? RevisionId { get; set; }
    public Revision? Revision { get; set; }
}
