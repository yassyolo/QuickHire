using QuickHire.Application.Gigs.Models.Details;
using QuickHire.Domain.Orders.Enums;

namespace QuickHire.Application.Orders.Models.Details;

public class OrderDetailsModel
{
    public OrderStatus CurrentStatus { get; set; }
    public List<OrderStatusStepModel> Steps { get; set; } = new();
    public List<GigRequirementAnswerModel> GigRequirements { get; set; } = new();
    public int GigId { get; set; }
    public string GigTitle { get; set; } = string.Empty;
    public string GigImageUrl { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public List<RevisionModel>? Revision { get; set; } 
    public PaymentPlanModel? Plan { get; set; }
    public int ConversationId { get; set; }
}

