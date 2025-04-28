using QuickHire.Domain.CustomOffers.Enums;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Orders;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.CustomOffers;

public class CustomOffer : BaseSoftDeletableEntity<int>
{
    public string CustomOfferNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Revisions { get; set; }
    public int DeliveryTimeInDays { get; set; }
    public int ExpiresInDays { get; set; }
    public string BuyerId { get; set; } = string.Empty;
    public string SellerId { get; set; } = string.Empty;
    public IEnumerable<PaymentPlanInclude> InclusiveServices { get; set; } = new List<PaymentPlanInclude>();
    public int GigId { get; set; }
    public Gig Gig { get; set; } = null!;
    public int MessageId { get; set; }
    public Message Message { get; set; } = null!;
    public CustomOfferStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? AcceptedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime? WithdrawnAt { get; set; }
    public string? WithdrawalReason { get; set; }
    public int? OrderId { get; set; } 
    public Order? Order { get; set; } 
    public int? ProjectBriefId { get; set; }
    public ProjectBrief? ProjectBrief { get; set; }
}

