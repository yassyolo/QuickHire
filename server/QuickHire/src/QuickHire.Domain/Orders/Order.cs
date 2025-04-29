using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Orders.Enums;
using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users;

namespace QuickHire.Domain.Orders;

public class Order : BaseSoftDeletableEntity<int>
{
    public string OrderNumber { get; set; } = string.Empty;
    public int? GigId { get; set; }
    public Gig? Gig { get; set; }
    public int? CustomeOfferId { get; set; }  
    public CustomOffer? CustomOffer { get; set; }
    public int SelectedPaymentPlanId { get; set; } 
    public PaymentPlan? SelectedPaymentPlan { get; set; } = null;
    public int BuyerId { get; set; } 
    public Buyer Buyer { get; set; } = null!;
    public int SellerId { get; set; } 
    public Seller Seller { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }  
    public OrderStatus Status { get; set; }
    public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
    public IEnumerable<Revision> Revisions { get; set; } = new List<Revision>();
    public int ConversationId { get; set; } 
    public Conversation Conversation { get; set; } = null!;
    public IEnumerable<GigRequirementAnswer> GigRequirementAnswers { get; set; } = new List<GigRequirementAnswer>();
    public int? InvoiceId { get; set; }  
    public Invoice? Invoice { get; set; } 
    public int? DeliveryId { get; set; }
    public Delivery? Delivery { get; set; } 
}

