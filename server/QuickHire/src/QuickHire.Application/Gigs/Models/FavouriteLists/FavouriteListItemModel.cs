using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Gigs.Models.FavouriteLists;
public class FavouriteListItemModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<GigCardModel> Gigs { get; set; } = new List<GigCardModel>();
}
