namespace QuickHire.Application.Gigs.Models.Details;
public class ReviewsForUserModel
{
    public string FullName { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}
