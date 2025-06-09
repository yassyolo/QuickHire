using QuickHire.Domain.Gigs;

namespace QuickHire.Application.Users.Models.ProjectBriefs;

public class GigScoreModel
{
    public int GigId { get; set; }
    public double Score { get; set; }
    public Gig Gig { get; set; } = null!;
    public int SellerId { get; set; }
}
