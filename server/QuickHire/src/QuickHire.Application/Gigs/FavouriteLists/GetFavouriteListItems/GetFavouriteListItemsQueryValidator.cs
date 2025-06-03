using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
namespace QuickHire.Application.Gigs.FavouriteLists.GetFavouriteListItems;

public class GetFavouriteListItemsQueryValidator : AbstractValidator<GetFavouriteListItemsQuery>
{
    public GetFavouriteListItemsQueryValidator()
    {
        RuleFor(x => x.Id)
.NotEmpty()
.WithMessage(string.Format(Required, "Id"))
.GreaterThan(0)
.WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

