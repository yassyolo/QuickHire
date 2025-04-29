namespace QuickHire.Domain.Shared.Exceptions;

public class InternalServerErrorException : BaseException
{
    public override int StatusCode => 500;
    public InternalServerErrorException(string message, string details) : base(message, details)
    {
    }
}
