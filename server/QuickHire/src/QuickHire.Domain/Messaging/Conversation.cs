using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Messaging;

public class Conversation : BaseSoftDeletableEntity<int>
{
    public string BuyerId { get; set; } = string.Empty;
    public string SellerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
    public DateTime LastMessageAt { get; set; } 
    public bool IsStarredByBuyer { get; set; }
    public bool IsStarredBySeller { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; } = null!;
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
}
