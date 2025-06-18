namespace QuickHire.Application.Users.Models.Messaging;

using System.Text.Json.Serialization;

public class CustomOfferPayloadModel
{
    [JsonPropertyName("gigTitle")]
    public string GigTitle { get; set; } = string.Empty;

    [JsonPropertyName("gigId")]
    public int GigId { get; set; }

    [JsonPropertyName("offerAmount")]
    public string OfferAmount { get; set; } = string.Empty;

    [JsonPropertyName("includes")]
    public List<string> Includes { get; set; } = new List<string>();

    [JsonPropertyName("offerId")]
    public int OfferId { get; set; }

    [JsonPropertyName("senderUsername")]
    public string SenderUsername { get; set; } = string.Empty;
}

