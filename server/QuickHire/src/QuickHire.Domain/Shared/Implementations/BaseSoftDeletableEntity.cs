using QuickHire.Domain.Shared.Contracts;

namespace QuickHire.Domain.Shared.Implementations;
public class BaseSoftDeletableEntity<TId> : BaseEntity<TId>, ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}