namespace QuickHire.Application.Users.Models.Gigs;
public class SellerGigTableRowModel
{
    public int Id { get; set; }
    public int Clicks { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Likes { get; set; }
    public int Orders { get; set; }
    public decimal Revenue { get; set; }
}
