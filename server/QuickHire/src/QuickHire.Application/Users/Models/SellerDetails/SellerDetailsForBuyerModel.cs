using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Models.SellerDetails;

public class SellerDetailsForBuyerModel
{
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public IEnumerable<UserLanguageModel> Languages { get; set; } = new List<UserLanguageModel>();
    public string Description { get; set; } = null!;
    public IEnumerable<SkillModel> Skills { get; set; } = new List<SkillModel>();
    public IEnumerable<CertificationModel> Certifications { get; set; } = new List<CertificationModel>();
    public IEnumerable<EducationModel> Education { get; set; } = new List<EducationModel>();
    public IEnumerable<PortfolioModel> Portfolios { get; set; } = new List<PortfolioModel>();
    public double AverageRating { get; set; }
    public bool TopRated { get; set; }
    public string Industry { get; set; } = string.Empty;
    public string MemberSince { get; set; } = string.Empty;
    public int TotalReviews { get; set; }
}
