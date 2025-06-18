using QuickHire.Domain.Orders.Enums;

namespace QuickHire.Application.Orders.Models.Details;


public class OrderStatusStepModel
{
    public string Name { get; set; } = null!;
    public OrderStatus Status { get; set; }
    public bool IsCompleted { get; set; }
}