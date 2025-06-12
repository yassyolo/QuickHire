using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
namespace QuickHire.Application.Users.Seller.CustomOffers.GetCustomOffer;

public class GetCustomOfferQueryValidator : AbstractValidator<GetCustomOfferQuery>
{
    public GetCustomOfferQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));
    }
}

