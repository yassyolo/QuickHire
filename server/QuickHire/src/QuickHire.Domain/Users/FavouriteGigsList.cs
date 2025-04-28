using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class FavouriteGigsList : BaseEntity<int>
{
    public string UserId { get; set; } = string.Empty;
    public IEnumerable<FavouriteGig> FavouriteGigs { get; set; } = new List<FavouriteGig>();
    public DateTime CreatedAt { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } 
}
