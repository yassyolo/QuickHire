using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class Portfolio : BaseSoftDeletableEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<string> ImageUrls { get; set; } = new List<string>();
    public string? VideoUrl { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

