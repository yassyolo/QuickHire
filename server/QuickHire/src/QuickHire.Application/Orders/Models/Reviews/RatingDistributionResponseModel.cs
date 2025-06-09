namespace QuickHire.Application.Orders.Models.Reviews;

public class RatingDistributionResponseModel
{
    public int Total { get; set; }
    public double Average { get; set; }
    public IEnumerable<RatingDistributionModel> Ratings { get; set; } = new List<RatingDistributionModel>();
}
