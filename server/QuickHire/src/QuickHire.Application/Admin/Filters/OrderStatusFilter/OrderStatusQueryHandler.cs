using Microsoft.AspNetCore.Mvc.Filters;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Domain.Moderation.Enums;
using QuickHire.Domain.Orders.Enums;
using System.Text.RegularExpressions;

namespace QuickHire.Application.Admin.Filters.OrderStatusFilter;

public class OrderStatusQueryHandler : IQueryHandler<OrderStatusQuery, FilterItemModel[]>
{
    public async Task<FilterItemModel[]> Handle(OrderStatusQuery request, CancellationToken cancellationToken)
    {
        return new FilterItemModel[]
       {
            new ()
            {
                Id = (int) OrderStatus.SubmittedRequirements,
                Name = SplitPascalCase(OrderStatus.SubmittedRequirements.ToString())
            },
            new ()
            {
                Id = (int) OrderStatus.PlacedOrder,
                Name = SplitPascalCase(OrderStatus.PlacedOrder.ToString())
            },
            new ()
            {
            Id = (int) OrderStatus.InProgress,
            Name = SplitPascalCase(OrderStatus.InProgress.ToString())
            },
            new ()
        {
            Id = (int) OrderStatus.Delivered,
            Name = SplitPascalCase(OrderStatus.Delivered.ToString())
        }
       };
    }

    private string SplitPascalCase(string input)
    {
        return Regex.Replace(input, @"(?<=[a-z])(?=[A-Z])", " ");
    }
}
