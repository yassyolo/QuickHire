using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users;

namespace QuickHire.Domain.Orders;

public class Invoice : BaseSoftDeletableEntity<int>
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public int BuyerId { get; set; } 
    public Buyer Buyer { get; set; } = null!;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
    public decimal TotalAmount { get; set; }
    public decimal Tax { get; set; }
}
