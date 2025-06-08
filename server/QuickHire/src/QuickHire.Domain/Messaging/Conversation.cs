using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users;

namespace QuickHire.Domain.Messaging;

public class Conversation : BaseSoftDeletableEntity<int>
{
    public string ParticipantAId { get; set; } = string.Empty;
    public string ParticipantAMode { get; set; } = string.Empty;

    public string ParticipantBId { get; set; } = string.Empty;
    public string ParticipantBMode { get; set; }  = string.Empty;
    public DateTime CreatedAt { get; set; } 
    public DateTime LastMessageAt { get; set; } 
    public bool IsStarredByBuyer { get; set; }
    public bool IsStarredBySeller { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; } = null!;
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
}
