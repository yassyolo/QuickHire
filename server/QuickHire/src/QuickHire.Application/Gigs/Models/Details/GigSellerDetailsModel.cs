using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Gigs.Models.Details;

public class GigSellerDetailsModel
{    
    public List<ReviewsForUserModel> Reviews { get; set; } = new List<ReviewsForUserModel>();
    public int Id { get; set; } 
    public string FullName { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool TopRated { get; set; }
    public int TotoalReviews { get; set; }
    public string Industry { get; set; } = string.Empty;
    public string MemberSince { get; set; } = string.Empty;
    public string LastDelivery { get; set; } = string.Empty;
    public List<string> Languages { get; set; } = new List<string>();
    public List<PortfolioModel> Portfolios { get; set; } = new List<PortfolioModel>();
}
