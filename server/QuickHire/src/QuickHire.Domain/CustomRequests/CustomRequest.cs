using QuickHire.Domain.Gigs;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.CustomRequests;

public class CustomRequest : BaseSoftDeletableEntity<int>
{
    public string CustomRequestNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public int GigId { get; set; }
    public Gig Gig { get; set; } = null!;
    public decimal Budget { get; set; } 
    public int DeliveryTimeInDays { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? RejectedAt { get; set; }
    public string? RejectedReason { get; set; } 
    public int MessageId { get; set; }
    public Message Message { get; set; } = null!;
    public IEnumerable<string>? AttachmentUrl { get; set; } = new List<string>();
}
