using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Orders;

public class OrderDeliveryDate : BaseSoftDeletableEntity<int>
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public DateTime DeliveryDate { get; set; } 
    public bool IsChanged { get; set; }
    public string? ChangeDateReason { get; set; } = string.Empty;
}
