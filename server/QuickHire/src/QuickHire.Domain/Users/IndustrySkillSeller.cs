using QuickHire.Domain.Categories;

namespace QuickHire.Domain.Users;

public class IndustrySkillSeller
{
    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
    public int IndustrySkillId { get; set; }
    public SubCategory IndustrySkill { get; set; } = null!;
}
