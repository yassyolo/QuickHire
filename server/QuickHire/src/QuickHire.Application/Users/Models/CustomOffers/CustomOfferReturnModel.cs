using QuickHire.Application.Users.Models.Messaging;

namespace QuickHire.Application.Users.Models.CustomOffers;

public class CustomOfferReturnModel
{
    public string Text { get; set; } = string.Empty;
    public int? ConversationId { get; set; }
    public CustomOfferPayloadModel Payload { get; set; } = new CustomOfferPayloadModel();
    public string ReceiverId { get; set; } = string.Empty;
}
