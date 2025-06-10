namespace QuickHire.Application.Users.Models.Messaging;

public class CurrentOrderModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string DueOn { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
