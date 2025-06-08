using QuickHire.Domain.Messaging;
using QuickHire.Domain.Orders.Enums;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Orders;

public class Revision : BaseSoftDeletableEntity<int>
{
    public string Description { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
    public DateTime? AcceptedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public RevisionStatus Status { get; set; }
    public string? RejectionReason { get; set; } = string.Empty;
    public IEnumerable<string> AttachmentUrls { get; set; } = new List<string>();
    public string? SourceFileUrl { get; set; } = string.Empty;
    public int MessageId { get; set; }
    public Message Message { get; set; } = null!;
    public bool IsAccepted { get; set; } 
}
