namespace QuickHire.Application.Users.Models.Messaging;

public class CustomOfferPayloadModel
{
    public string GigTitle { get; set; } = string.Empty;
    public int GigId { get; set; }
    public string OfferAmount { get; set; } = string.Empty;
    public List<string> Includes { get; set; } = new List<string>();
    public int OfferId { get; set; }
    public string SenderUsername { get; set; } = string.Empty;
}
