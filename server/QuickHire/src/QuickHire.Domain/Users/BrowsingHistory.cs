using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class BrowsingHistory : BaseEntity<int>
{
    public int BuyerId { get; set; } 
    public Buyer Buyer { get; set; } = null!;
    public int? GigId { get; set; } 
    public Gig? Gig { get; set; } 
    public int? SellerId { get; set; }
    public Seller? Seller { get; set; } 
    public DateTime ViewedAt { get; set; } 
}
