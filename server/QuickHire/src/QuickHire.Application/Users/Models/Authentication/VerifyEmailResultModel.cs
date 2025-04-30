
namespace QuickHire.Application.Users.Models.Authentication;

public class VerifyEmailResultModel
{
    public bool IsSuccess { get; set; }
    public string[] Errors { get; set; } = Array.Empty<string>();
}
