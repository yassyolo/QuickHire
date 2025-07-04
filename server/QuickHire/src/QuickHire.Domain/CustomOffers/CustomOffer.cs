﻿using QuickHire.Domain.CustomOffers.Enums;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Orders;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users;

namespace QuickHire.Domain.CustomOffers;

public class CustomOffer : BaseSoftDeletableEntity<int>
{
    public string CustomOfferNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Revisions { get; set; }
    public int DeliveryTimeInDays { get; set; }
    public int BuyerId { get; set; } 
    public Buyer Buyer { get; set; } = null!;
    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
    public IEnumerable<PaymentPlanInclude> InclusiveServices { get; set; } = new List<PaymentPlanInclude>();
    public int GigId { get; set; }
    public Gig Gig { get; set; } = null!;
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

