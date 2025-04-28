using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Domain.Users;

public class Skill : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public SkillLevel Level { get; set; } 
    public string UserId { get; set; } = string.Empty;
}
