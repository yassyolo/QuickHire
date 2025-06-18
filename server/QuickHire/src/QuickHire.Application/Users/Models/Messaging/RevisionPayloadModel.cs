using MediatR.NotificationPublishers;

namespace QuickHire.Application.Users.Models.Messaging;

public class RevisionPayloadModel
{
    public string[] Attachments { get; set; } = Array.Empty<string>();
    public string Description { get; set; } = string.Empty;
    public string SourceFileUrl { get; set; } = string.Empty;
    public int RevisionNumber { get; set; }
    public string AcceptUntil { get; set; } = string.Empty;
    public int RevisionId { get; set; }
}
