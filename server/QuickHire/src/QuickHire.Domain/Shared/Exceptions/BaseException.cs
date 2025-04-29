namespace QuickHire.Domain.Shared.Exceptions;

public abstract class BaseException : Exception
{
    public BaseException(string message, string? details = null) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
    public virtual int StatusCode { get; }
}

