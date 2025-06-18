namespace QuickHire.Application.Users.Models.Messaging;

public class DeliveryPayloadModel
{
    public string[] Attachments { get; set; } = Array.Empty<string>();
    public string Description { get; set; } = string.Empty;
    public string? SourceFileUrl { get; set; } = string.Empty;
}
