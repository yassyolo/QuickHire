using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Orders;

public class Invoice : BaseSoftDeletableEntity<int>
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
    public decimal TotalAmount { get; set; }
    public decimal Tax { get; set; }
}
