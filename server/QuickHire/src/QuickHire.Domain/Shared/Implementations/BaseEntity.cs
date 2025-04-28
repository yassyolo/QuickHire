using QuickHire.Domain.Shared.Contracts;

namespace QuickHire.Domain.Shared.Implementations;

public class BaseEntity<T> : IBaseEntity<T>
{
    public T Id { get; set; } = default!;
}
