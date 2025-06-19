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
    public ReviewModel? Review { get; set; }
    public PaymentPlanModel? Plan { get; set; }
    public int ConversationId { get; set; }
    public OrderDeliveryModel? Delivery { get; set; } 
}
public class OrderDeliveryModel
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Attachments { get; set; } = new();
    public string DateCreated { get; set; } = string.Empty;
}

public class ReviewModel
{
    public string FullName { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public bool RepeatBuyer { get; set; }  
}

