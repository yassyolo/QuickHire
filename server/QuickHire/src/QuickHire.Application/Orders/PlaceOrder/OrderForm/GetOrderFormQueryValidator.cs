using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using Newtonsoft.Json;

namespace QuickHire.Application.Orders.PlaceOrder.OrderForm;

public class GetOrderFormQueryValidator : AbstractValidator<GetOrderFormQuery>
{
    public GetOrderFormQueryValidator()
    {
        RuleFor(x => x.GigId)
            .NotEmpty()
            .WithMessage(string.Format(Common.Constants.ValidationMessages.Required, "Id"));
    }
}

