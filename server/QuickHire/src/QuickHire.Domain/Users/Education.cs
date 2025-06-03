using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Domain.Users;

public class Education : BaseSoftDeletableEntity<int>
{
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public int GraduationYear { get; set; }
    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
}
