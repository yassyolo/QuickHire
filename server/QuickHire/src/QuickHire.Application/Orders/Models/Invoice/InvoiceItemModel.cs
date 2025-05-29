namespace QuickHire.Application.Orders.Models.Invoice;

public class InvoiceItemModel
{
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string TotalAmount { get; set; } = string.Empty;
}
