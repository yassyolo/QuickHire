namespace QuickHire.Application.Gigs.Models.Details;

public class GigDetailsModel
{
    public int MainCategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string MainCategoryName { get; set; } = string.Empty;
    public string SubCategoryName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] ImageUrls { get; set; } = Array.Empty<string>();
    public PaymentPlanModel[] PaymentPlans { get; set; } = Array.Empty<PaymentPlanModel>();
    public GigMetaDataModel[] GigMetaData { get; set; } = Array.Empty<GigMetaDataModel>();
    public int OrdersInQueue { get; set; }
    public int NumberOfLikes { get; set; }
    public bool Liked { get; set; }
    public int SellerId { get; set; }
}
