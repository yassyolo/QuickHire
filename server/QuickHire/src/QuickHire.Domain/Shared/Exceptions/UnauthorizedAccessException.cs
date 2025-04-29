namespace QuickHire.Domain.Shared.Exceptions;

public class UnauthorizedAccessException : BaseException
{
    public override int StatusCode => 401;  
    public UnauthorizedAccessException(string message, string? details = null) : base(message, details)
    {
    }
}
