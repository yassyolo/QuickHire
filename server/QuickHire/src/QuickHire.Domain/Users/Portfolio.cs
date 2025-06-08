using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class Portfolio : BaseSoftDeletableEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int SellerId { get; set; } 
    public Seller Seller { get; set; } = null!;
    public int MainCategoryId { get; set; }
    public MainCategory MainCategory { get; set; } = null!;

}

