namespace QuickHire.Application.Users.Models.Statistics;

public class GigPerformanceRowModel
{
    public string Date { get; set; } = null!;
    public int GigsSold { get; set; }
    public int GigLikes { get; set; }
    public int GigFavourites { get; set; }
    public int RepeatViews { get; set; }
    public int CommentsCount { get; set; }
}
