using System.Text.Json.Serialization;

namespace QuickHire.Application.Users.Models.Messaging
{
    public class RevisionPayloadModel
    {
        [JsonPropertyName("attachment")]
        public string Attachment { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("revisionNumber")]
        public int RevisionNumber { get; set; }

        [JsonPropertyName("revisionId")]
        public int RevisionId { get; set; }
    }
}
