using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class BillingDetails : BaseEntity<int>
{
    public string FullName { get; set; } = string.Empty;
    public Address Address { get; set; } = null!;
    public int AddressId { get; set; }
    public string? CompanyName { get; set; } 
    public string UserId { get; set; } = string.Empty;
}
