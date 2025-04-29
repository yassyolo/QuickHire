using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class Portfolio : BaseSoftDeletableEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; } = string.Empty;
    public int SellerId { get; set; } 
    public Seller Seller { get; set; } = null!;
}

