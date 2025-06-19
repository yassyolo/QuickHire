using QuickHire.Domain.Messaging;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Orders;

public class Delivery : BaseSoftDeletableEntity<int>
{
    public string Description { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
    public List<string> AttachmentUrls { get; set; } = new List<string>();
    public string? SourceFileUrl { get; set; } = string.Empty;
}
