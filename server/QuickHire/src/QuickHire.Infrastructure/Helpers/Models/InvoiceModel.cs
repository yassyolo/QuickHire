namespace QuickHire.Infrastructure.Helpers.Models;

public class InvoiceModel
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public string BuyerName { get; set; } = string.Empty;
    public string BuyerAddress { get; set; } = string.Empty;
    public string BuyerCompanyName { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public IEnumerable<InvoiceItemModel> Items { get; set; } = new List<InvoiceItemModel>();
    public string TotalAmount { get; set; } = string.Empty;
    public string SubTotal { get; set; } = string.Empty;
    public string Tax { get; set; } = string.Empty;
}
