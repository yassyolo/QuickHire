using QuickHire.Domain.Orders.Enums;

namespace QuickHire.Application.Orders.Models.Details;

public class OrderDetailsModel
{
    public OrderStatus CurrentStatus { get; set; }
    public List<OrderStatusStepModel> Steps { get; set; } = new();
    public List<GigRequirementAnswerModel> GigRequirements { get; set; } = new();
}

