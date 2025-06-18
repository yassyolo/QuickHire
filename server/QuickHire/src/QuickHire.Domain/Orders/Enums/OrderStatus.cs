namespace QuickHire.Domain.Orders.Enums;

public enum OrderStatus 
{
    PlacedOrder = 0,
    SubmittedRequirements = 1,
    PendingPayment = 2,
    Paid = 3,
    InProgress = 4,
    Delivered = 5,
    Failed = 6,
}
