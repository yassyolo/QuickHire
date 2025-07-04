﻿using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Orders;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class Buyer : BaseEntity<int>
{
    public string UserId { get; set; } = string.Empty;
    public IEnumerable<Notification>? Notifications { get; set; }
    public IEnumerable<BrowsingHistory>? BrowsingHistories { get; set; }
    public IEnumerable<FavouriteGigsList>? FavouriteGigsLists { get; set; }
    public IEnumerable<Invoice>? Invoices { get; set; }   
    public IEnumerable<Order>? PlacedOrders { get; set;}
    public IEnumerable<CustomOffer>? CustomOffers { get; set; }
    public IEnumerable<ProjectBrief>? ProjectBriefs { get; set; }
    public IEnumerable<FavouriteGig>? FavouriteGigs { get; set; }
}
