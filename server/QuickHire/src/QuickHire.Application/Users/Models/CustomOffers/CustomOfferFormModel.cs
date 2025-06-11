namespace QuickHire.Application.Users.Models.CustomOffers;

public class CustomOfferFormModel
{
    public ChooseFromGigsModel[] Gigs { get; set; } = Array.Empty<ChooseFromGigsModel>();
    public InclusivesModel[] Inclusives { get; set; } = Array.Empty<InclusivesModel>();
}
