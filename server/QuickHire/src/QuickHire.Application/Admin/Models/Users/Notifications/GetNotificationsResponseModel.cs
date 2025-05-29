namespace QuickHire.Application.Admin.Models.Users.Notifications;

public class GetNotificationsResponseModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
}
