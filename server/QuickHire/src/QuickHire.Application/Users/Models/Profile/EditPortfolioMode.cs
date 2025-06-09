using Microsoft.AspNetCore.Http;

namespace QuickHire.Application.Users.Models.Profile;

public class EditPortfolioMode
{
    public int Id { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MainCategoryId { get; set; }
    public IFormFile? Image { get; set; }
}
