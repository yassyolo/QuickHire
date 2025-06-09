namespace QuickHire.Application.Gigs.Models.Shared;

public class GigCardModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal FromPrice { get; set; }
    public IEnumerable<string> ImageUrls { get; set; } = new List<string>();
    public string SellerName { get; set; } = null!;
    public int SellerId { get; set; }
    public string SellerProfileImageUrl { get; set; } = null!;
    public bool TopRatedSeller { get; set; }
    public int ReviewsCount { get; set; }
    public double AverageRating { get; set; }
    public bool Liked { get; set; }
}
