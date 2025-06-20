﻿using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class FavouriteGig : BaseEntity<int>
{
    public int GigId { get; set; }
    public int BuyerId { get; set; }
    public Buyer Buyer { get; set; } = null!;
    public Gig Gig { get; set; } = null!;
    public DateTime AddedAt { get; set; }
    public int FavouriteGigsListId { get; set; } 
    public FavouriteGigsList FavouriteGigsList { get; set; } = null!;
}
