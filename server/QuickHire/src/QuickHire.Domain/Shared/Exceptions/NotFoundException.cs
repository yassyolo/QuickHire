namespace QuickHire.Domain.Shared.Exceptions;

public class NotFoundException : BaseException
{
    public override int StatusCode => 404;
    public NotFoundException(string name, object key) : 
        base($"Entity '{name}' ({key}) was not found.")
    {
    }
}

