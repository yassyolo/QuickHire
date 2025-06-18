namespace QuickHire.Application.Users.Models.ProjectBriefs;

public class BuyerProjectBriefModel
{
    public int Id { get; set; }
    public string Date { get; set; } = null!;
    public string DocumentNumber { get; set; } = null!;
    public int SellersReached { get; set; } 
    public int TotalOffers { get; set; }
    public string Order { get; set; } = null!;
    public string Status { get; set; } = null!;
}
