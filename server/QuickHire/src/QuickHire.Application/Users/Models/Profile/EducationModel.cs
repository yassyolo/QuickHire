namespace QuickHire.Application.Users.Models.Profile;

public class EducationModel
{
    public int Id { get; set; }
    public string Institution { get; set; } = null!;
    public string Degree { get; set; } = null!;
    public string EndYear { get; set; } = null!; 
    public string Major { get; set; } = null!;
}
