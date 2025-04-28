using Microsoft.AspNetCore.Identity;
using QuickHire.Domain.Users;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Moderation.Enums;

namespace QuickHire.Infrastructure.Persistence.Identity;

public class ApplicationUser : IdentityUser<string>
{
    public string FullName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime JoinedAt { get; set; }
    public string? ProfileImageUrl { get; set; } = string.Empty;
    public IEnumerable<Notification>? Notifications { get; set; }
    public IEnumerable<UserLanguage>? Languages { get; set; }
    public IEnumerable<FAQ>? FAQs { get; set; }
    public ModerationStatus ModerationStatus { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }
    public string? RefreshToken { get; set; } 
}
