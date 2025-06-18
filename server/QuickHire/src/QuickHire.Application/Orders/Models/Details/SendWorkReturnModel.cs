using QuickHire.Application.Users.Models.Messaging;

namespace QuickHire.Application.Orders.Models.Details;

public class SendWorkReturnModel
{
    public DeliveryPayloadModel? Delivery { get; set; } 
    public RevisionPayloadModel? Revision { get; set; }
}
