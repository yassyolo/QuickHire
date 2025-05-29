namespace QuickHire.Application.Admin.Models.FAQ;

public class FAQResponseModel
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}
