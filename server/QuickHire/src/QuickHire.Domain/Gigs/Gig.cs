using QuickHire.Domain.Categories;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.CustomRequests;
using QuickHire.Domain.Moderation.Enums;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users;

namespace QuickHire.Domain.Gigs;
public class  Gig : BaseSoftDeletableEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new List<string>();
    public string? VideoUrl { get; set; } = string.Empty;
    public int SellerId { get; set; } 
    public Seller Seller { get; set; } = null!;
    public int SubSubCategoryId { get; set; }
    public DateTime CreatedAt { get; set; } 
    public int Clicks { get; set; }
    public SubSubCategory SubSubCategory { get; set; } = null!;
    public ModerationStatus ModerationStatus { get; set; } 
    public IEnumerable<FAQ> FAQs { get; set; } = new List<FAQ>();
    public IEnumerable<GigMetadata> Metadata { get; set; } = new List<GigMetadata>();
    public IEnumerable<GigRequirement> Requirements { get; set; } = new List<GigRequirement>();
    public IEnumerable<PaymentPlan> PaymentPlans { get; set; } = new List<PaymentPlan>();
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    public IEnumerable<CustomOffer> CustomOffers { get; set; } = new List<CustomOffer>();
    public IEnumerable<CustomRequest> CustomRequests { get; set; } = new List<CustomRequest>();
}