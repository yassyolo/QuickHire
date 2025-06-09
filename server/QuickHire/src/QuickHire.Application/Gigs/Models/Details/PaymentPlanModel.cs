namespace QuickHire.Application.Gigs.Models.Details;

public class PaymentPlanModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DeliveryTimeInDays { get; set; }
    public int Revisions { get; set; }
    public List<PaymentPlanIncludeModel> Inclusions { get; set; } = new();
}
