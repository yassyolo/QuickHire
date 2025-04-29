using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class Certification : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; } 
    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
}

