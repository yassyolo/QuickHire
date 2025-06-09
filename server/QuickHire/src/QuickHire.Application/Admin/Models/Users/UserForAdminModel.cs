namespace QuickHire.Application.Admin.Models.Users;

public class UserForAdminModel
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Roles { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Joined { get; set; } = null!;
}
