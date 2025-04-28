using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class BrowsingHistory : BaseEntity<int>
{
    public string UserId { get; set; } = string.Empty;
    public int GigId { get; set; } 
    public Gig Gig { get; set; } = null!;
    public DateTime ViewedAt { get; set; } = DateTime.Now;
}
