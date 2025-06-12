namespace QuickHire.Application.Users.Models.CustomOffers;

public class CustomOfferPreviewModel
{
        public string CustomOfferNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string Revisions { get; set; } = string.Empty;
    public string DeliveryTimeInDays { get; set; } = string.Empty;
    public string[] OfferIncludes { get; set; } = Array.Empty<string>();
    public string CreateOn { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public string SellerProfilePictureUrl { get; set; } = string.Empty;
    public string MemberSince { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string[] Languages { get; set; } = Array.Empty<string>();
}
