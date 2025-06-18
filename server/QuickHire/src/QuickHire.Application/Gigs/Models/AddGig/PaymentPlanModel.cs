using QuickHire.Application.Gigs.Models.Details;

namespace QuickHire.Application.Gigs.Models.AddGig;

public class PaymentPlanModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DeliveryTimeInDays { get; set; }
    public int Revisions { get; set; }
    public PaymentPlanIncludeModel[] Inclusions { get; set; } = Array.Empty<PaymentPlanIncludeModel>();
}
