namespace QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;

public class GetBillingInfoModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int CountryId { get; set; }
}
