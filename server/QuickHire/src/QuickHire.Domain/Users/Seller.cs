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
    public IEnumerable<Portfolio>? Portfolios { get; set; } 
    public IEnumerable<Certification>? Certifications { get; set; }
    public IEnumerable<Education>? Educations { get; set; }
    public IEnumerable<Skill>? Skills { get; set; }
    public IEnumerable<Gig>? Gigs { get; set; }
    public IEnumerable<Conversation>? Conversations { get; set; }
    public IEnumerable<Order>? SoldOrders { get; set; }
    public IEnumerable<CustomOffer>? CustomOffers { get; set; }
}

