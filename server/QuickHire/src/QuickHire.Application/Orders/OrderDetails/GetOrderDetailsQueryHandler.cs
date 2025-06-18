using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Orders.Models.Details;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Orders.Enums;
using QuickHire.Domain.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace QuickHire.Application.Orders.OrderDetails;

public class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, OrderDetailsModel>
{
    private readonly IRepository _repository;

    public GetOrderDetailsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrderDetailsModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var orderQueryable = _repository.GetAllIncluding<Domain.Orders.Order>().Where(x => x.Id == request.Id);
        var order = await _repository.FirstOrDefaultAsync(orderQueryable);
        if (order == null)
        {
            throw new NotFoundException(nameof(Domain.Orders.Order), request.Id)
;       }

        var gigRequirementAnswers = _repository.GetAllIncluding<GigRequirementAnswer>(x => x.GigRequirement).Where(x => x.OrderId == request.Id);
        var gigRequirements = await _repository.ToListAsync<GigRequirementAnswer>(gigRequirementAnswers);


        var steps = Enum.GetValues<OrderStatus>()
    .SkipLast(1)
    .Select(status => new OrderStatusStepModel
    {
        Status = status,
        Name = SplitPascalCase(status.ToString()),
        IsCompleted = (int)status <= (int)order.Status
    })
    .ToList();


        var orderDetailsModel = new OrderDetailsModel
        {
            GigRequirements = gigRequirements.Select(x => new GigRequirementAnswerModel
            {
                Answer = x.Answer,
                Question = x.GigRequirement.Question,
            }).ToList(),
            
            CurrentStatus = order.Status,
            Steps = steps
    };

        return orderDetailsModel;
    }

    private string SplitPascalCase(string input)
    {
        return Regex.Replace(input, @"(?<=[a-z])(?=[A-Z])", " ");
    }
}
