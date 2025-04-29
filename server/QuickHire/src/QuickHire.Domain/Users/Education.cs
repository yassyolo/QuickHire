using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Domain.Users;

public class Education : BaseSoftDeletableEntity<int>
{
    public EducationDegree Degree { get; set; } 
    public string Institution { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
}
