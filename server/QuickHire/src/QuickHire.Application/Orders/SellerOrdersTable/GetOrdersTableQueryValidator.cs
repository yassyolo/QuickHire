﻿using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Orders.GetOrdersTable;

public class GetOrdersTableQueryValidator : AbstractValidator<GetOrdersTableQuery>
{
    public GetOrdersTableQueryValidator()
    {
        RuleFor(x => x.OrderStatusId)
            .NotEmpty()
            .WithMessage(string.Format(Required, "OrderStatusId"));
    }
}

