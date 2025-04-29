namespace QuickHire.Domain.Shared.Exceptions;

public class BadRequestException : BaseException
{
    public override int StatusCode => 400;

    public BadRequestException(string message, string details) : base(message, details)
    {
    }
}
