using System.Text.Json.Serialization;

namespace QuickHire.Application.Users.Models.Messaging
{
    public class DeliveryPayloadModel
    {
        [JsonPropertyName("attachment")]
        public string Attachment { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("sourceFileUrl")]
        public string? SourceFileUrl { get; set; } = string.Empty;
    }
}
