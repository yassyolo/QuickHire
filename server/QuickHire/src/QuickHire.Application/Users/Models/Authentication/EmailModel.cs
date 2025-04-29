namespace QuickHire.Application.Users.Models.Authentication;

public class EmailModel
{
    public string Body { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
}
