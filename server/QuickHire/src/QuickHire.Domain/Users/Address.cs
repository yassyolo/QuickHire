using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Users;

public class Address : BaseSoftDeletableEntity<int>
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool IsBillingAddress { get; set; } = false;
    public Country Country { get; set; } = null!;
    public int CountryId { get; set; }
}
