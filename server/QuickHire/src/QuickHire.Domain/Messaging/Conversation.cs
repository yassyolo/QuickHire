using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users;

namespace QuickHire.Domain.Messaging;

public class Conversation : BaseSoftDeletableEntity<int>
{
    public int BuyerId { get; set; } 
    public Buyer Buyer { get; set; } = null!;
    public int SellerId { get; set; } 
    public Seller Seller { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
    public DateTime LastMessageAt { get; set; } 
    public bool IsStarredByBuyer { get; set; }
    public bool IsStarredBySeller { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; } = null!;
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
}
