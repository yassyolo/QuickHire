using QuickHire.Domain.Users.Enums;

namespace QuickHire.Application.Users.Models.Notifications;

public class NotificationModel
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public string UserId { get; set; } = string.Empty;
}
