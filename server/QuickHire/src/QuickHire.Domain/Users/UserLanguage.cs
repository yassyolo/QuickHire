using QuickHire.Domain.Users.Enums;

namespace QuickHire.Domain.Users;

public class UserLanguage
{
    public string UserId { get; set; } = string.Empty;
    public int LanguageId { get; set; }
    public Language Language { get; set; } = null!;
}
