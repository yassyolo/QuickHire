namespace QuickHire.Application.Orders.Models.Reviews;

public class ReviewResponseRowModel
{
    public string Date { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public bool RepeatBuyer { get; set; }
}
