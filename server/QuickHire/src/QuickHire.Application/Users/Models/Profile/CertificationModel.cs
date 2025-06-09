namespace QuickHire.Application.Users.Models.Profile;
public class CertificationModel
{
    public int Id { get; set; }
    public string Certification { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Date { get; set; } = null!;
}
