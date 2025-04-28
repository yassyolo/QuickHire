using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class Language : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
}
