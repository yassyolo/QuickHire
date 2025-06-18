using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Users.Models.Gigs;

public class HotGigsModel
{
    public List<GigCardModel> Gigs { get; set; } = new List<GigCardModel>();
    public string MainCategory { get; set; } = string.Empty;
}
