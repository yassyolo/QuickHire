namespace QuickHire.Application.Orders.Models.Details;

public class RevisionModel
{
    public int Id { get; set; }
    public int RevisionNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public string SourceFileUrl { get; set; } = string.Empty;
    public IEnumerable<string> Attachments { get; set; } = new List<string>();
    public string DateCreated { get; set; } = string.Empty;
}
