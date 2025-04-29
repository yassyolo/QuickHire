using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Orders;

public class Review : BaseSoftDeletableEntity<int>
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public string CreatorUserId { get; set; } = string.Empty;
}
