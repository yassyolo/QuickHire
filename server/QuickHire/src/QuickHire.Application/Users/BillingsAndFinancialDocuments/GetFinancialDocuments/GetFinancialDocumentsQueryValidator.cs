using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.GetFinancialDocuments;

public class GetFinancialDocumentsQueryValidator : AbstractValidator<GetFinancialDocumentsQuery>
{
    public GetFinancialDocumentsQueryValidator()
    {
        RuleFor(x => x.Buyer)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Buyer"));
    }
}

