namespace QuickHire.Application.Orders.Models.Form;

public class PaymentIntentResponse
{
    public string ClientSecret { get; set; }
    public int OrderId { get; set; }
}
