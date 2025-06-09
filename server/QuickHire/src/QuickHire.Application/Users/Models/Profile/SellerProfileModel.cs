namespace QuickHire.Application.Users.Models.Profile;

public class SellerProfileModel
{
    public string ProfilePictureUrl { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Username { get; set; } = null!;
    public IEnumerable<UserLanguageModel> Languages { get; set; } = new List<UserLanguageModel>();
    public string Description { get; set; } = null!;
    public IEnumerable<SkillModel> Skills { get; set; } = new List<SkillModel>();
    public IEnumerable<CertificationModel> Certifications { get; set; } = new List<CertificationModel>();
    public IEnumerable<EducationModel> Education { get; set; } = new List<EducationModel>();
    public IEnumerable<PortfolioModel> Portfolios { get; set; } = new List<PortfolioModel>();
}
