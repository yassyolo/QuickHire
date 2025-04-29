using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class FavouriteGigsList : BaseEntity<int>
{
    public int BuyerId { get; set; } 
    public Buyer Buyer { get; set; } = null!;
    public IEnumerable<FavouriteGig> FavouriteGigs { get; set; } = new List<FavouriteGig>();
    public DateTime CreatedAt { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } 
}
