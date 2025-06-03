using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Domain.Users;

public class Skill : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
}
