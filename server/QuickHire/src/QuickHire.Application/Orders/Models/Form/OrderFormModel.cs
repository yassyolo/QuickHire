

using QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;


namespace QuickHire.Application.Orders.Models.Form;

public class OrderFormModel
{
    public List<GigRequirementModel> GigRequirements { get; set; } = new();
    public GetBillingInfoModel BillingDetails { get; set; } = new GetBillingInfoModel();
}
