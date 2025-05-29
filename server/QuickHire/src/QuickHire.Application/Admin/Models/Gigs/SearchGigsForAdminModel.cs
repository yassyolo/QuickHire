namespace QuickHire.Application.Admin.Models.Gigs;
public class SearchGigsForAdminModel
{
    public int Id { get; set; }
    public string CreatedOn { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public int Orders { get; set; }
    public decimal Revenue { get; set; }
    public int Clicks { get; set; }
    public decimal AvgReview { get; set; }
    public string SubSubCategoryName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
