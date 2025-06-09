namespace QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;
public class FinancialDocumentRowModel
{
    public int Id { get; set; }
    public string Date { get; set; } = null!;
    public string DocumentNumber { get; set; } = null!;
    public string Service { get; set; } = null!;
    public string OrderNumber { get; set; } = null!;
    public string Total { get; set; } = null!;
    public string PdfLink { get; set; } = null!;
}
