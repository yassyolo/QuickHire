namespace QuickHire.Application.Users.Models.NotAuthenticated;

public class NotAuthenticatedPageModel
{

    public PopularServicesModel[] PopularServices { get; set; } = Array.Empty<PopularServicesModel>();
    public string[] PopularTags { get; set; } = Array.Empty<string>();
}
