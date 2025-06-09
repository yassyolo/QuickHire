namespace QuickHire.Application.Users.Models.Profile;

public class PortfolioModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int MainCategoryId { get; set; }
    public string MainCategoryName { get; set; } = null!;
}
