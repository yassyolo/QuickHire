using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Domain.Users;

public class Notification : BaseSoftDeletableEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
    public bool IsRead { get; set; }
    public NotificationType NotificationType { get; set; }
    public string UserId { get; set; } = string.Empty;
    public bool Sent { get; set; }
}

