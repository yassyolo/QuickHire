namespace QuickHire.Application.Users.Models.Messaging;
public class CurrentConversationModel
{
    public int Id { get; set; }
    public List<MessagesForConversationModel> Messages { get; set; } = new List<MessagesForConversationModel>();
    public bool IsStarred { get; set; }
    public ParticipantBInfoModel ParticipantBInfo { get; set; } = new();
    public CurrentOrderModel? CurrentOrder { get; set; }
}
