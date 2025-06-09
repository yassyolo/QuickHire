
namespace QuickHire.Application.Gigs.Models.FavouriteLists;

public class FavouriteListModel
{
public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<string> ImageUrls { get; set; } = new List<string>();
    public int GigCount { get; set; }
}
