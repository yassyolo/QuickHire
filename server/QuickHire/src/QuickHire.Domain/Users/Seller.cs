using QuickHire.Domain.Categories;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Orders;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Shared.Implementations;
namespace QuickHire.Domain.Users;
public class Seller : BaseEntity<int>
{
    public string UserId { get; set; } = string.Empty;
    public int Clicks { get; set; }
    public int IndustryId { get; set; }
    public MainCategory Industry { get; set; } = null!;
    public IEnumerable<Notification>? Notifications { get; set; }
    public IEnumerable<Portfolio>? Portfolios { get; set; } = new List<Portfolio>();
    public IEnumerable<Certification>? Certifications { get; set; } = new List<Certification>();
    public IEnumerable<Education>? Educations { get; set; } = new List<Education>();
    public IEnumerable<Skill>? Skills { get; set; } = new List<Skill>();
    public IEnumerable<Gig>? Gigs { get; set; }
    public IEnumerable<Order>? SoldOrders { get; set; }
    public IEnumerable<CustomOffer>? CustomOffers { get; set; }
}

