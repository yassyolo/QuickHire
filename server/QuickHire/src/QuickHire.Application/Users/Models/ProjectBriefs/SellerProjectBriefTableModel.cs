namespace QuickHire.Application.Users.Models.ProjectBriefs;

public class SellerProjectBriefTableModel
{
    public int Id { get; set; }
    public string BuyerUsername { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DeliveryTimeInDays { get; set; } = string.Empty;
    public decimal Budget { get; set; }
}
