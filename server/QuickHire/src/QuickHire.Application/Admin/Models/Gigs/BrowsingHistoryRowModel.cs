namespace QuickHire.Application.Admin.Models.Gigs;

public class BrowsingHistoryRowModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Liked { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
