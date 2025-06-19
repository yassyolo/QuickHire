using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Details;
using QuickHire.Application.Orders.Models.Details;
using QuickHire.Application.Orders.Models.Reviews;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Orders.Enums;
using QuickHire.Domain.Shared.Exceptions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace QuickHire.Application.Orders.OrderDetails;

public class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, OrderDetailsModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetOrderDetailsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<OrderDetailsModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var orderQueryable = _repository.GetAllIncluding<Domain.Orders.Order>(x => x.Gig).Where(x => x.Id == request.Id);
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

        var revisionsQueryable = _repository.GetAllIncluding<Domain.Orders.Revision>().Where(x => x.OrderId == request.Id).OrderBy(x => x.CreatedAt);
        var revisions = await _repository.ToListAsync<Domain.Orders.Revision>(revisionsQueryable);
        var revisionModels = default(List<RevisionModel>);
        if (revisions.Any()) {
            revisionModels = revisions
                .Select((x, index) => new RevisionModel
                {
                    Id = x.Id,
                    RevisionNumber = index + 1,
                    Description = x.Description,
                    SourceFileUrl = x.SourceFileUrl ?? string.Empty,
                    Attachments = x.AttachmentUrls?.ToList(),
                    DateCreated = x.CreatedAt.ToString("dd MMMM yyyy")
                })
                .ToList();
        }

        var reviewsQueryable = _repository.GetAllIncluding<Domain.Orders.Review>(x => x.Order.Buyer, x => x.Order.Seller, x => x.Order.Gig, x => x.Order.SelectedPaymentPlan).Where(x => x.OrderId == request.Id);
        var review = await _repository.FirstOrDefaultAsync<QuickHire.Domain.Orders.Review>(reviewsQueryable);
        var reviewModel = default(ReviewModel);

        if(review != null)
        {
            var userDetails = await _userService.GetBuyerReviewDetailsAsync(review.Order.BuyerId, review.Order.SellerId);
            reviewModel = new ReviewModel
            {
                FullName = userDetails.fullName,
                ProfileImageUrl = userDetails.profileImageUrl,
                CountryName = userDetails.countryName,
                RepeatBuyer = userDetails.repeatBuyer,
                Date = review.CreatedOn.ToString("dd MMMM yyyy"),
                Rating = review.Rating,
                Comment =review.Comment,
                Duration = review.Order.SelectedPaymentPlan.DeliveryTimeInDays.ToString(),
                Price = review.Order.TotalPrice.ToString("C"),
            };
        }    

        var deliveryQueryable = _repository.GetAllReadOnly<Domain.Orders.Delivery>().Where(x => x.OrderId == request.Id);
        var delivery = await _repository.FirstOrDefaultAsync(deliveryQueryable);
        var deliveryModel = default(OrderDeliveryModel);
        if (delivery != null)
        {
            deliveryModel = new OrderDeliveryModel
            {
                Id = delivery.Id,
                Description = delivery.Description,
                Attachments = delivery.AttachmentUrls.ToList(),
                DateCreated = delivery.CreatedAt.ToString("dd MMMM yyyy")
            };
        }
       
        var paymentPlansQueryable = _repository.GetAllIncluding<QuickHire.Domain.Gigs.PaymentPlan>(x => x.Inclusions).Where(x => x.GigId == order.GigId);
        var paymentPlans = await _repository.FirstOrDefaultAsync(paymentPlansQueryable);
        if (paymentPlans == null)
        {
            throw new NotFoundException(nameof(QuickHire.Domain.Gigs.PaymentPlan), order.GigId);
        }
        var paymentPlanmodel = new PaymentPlanModel
        {
            Id = paymentPlans.Id,
            Name = paymentPlans.Name,
            Price = paymentPlans.Price,
            Description = paymentPlans.Description,
            DeliveryTimeInDays = paymentPlans.DeliveryTimeInDays,
            Revisions = paymentPlans.Revisions,
            Inclusions = paymentPlans.Inclusions.Select(x => new QuickHire.Application.Gigs.Models.Details.PaymentPlanIncludeModel
            {
                Name = x.Name,
                Value = x.Value
            }).ToList()
        };      

        var orderDetailsModel = new OrderDetailsModel
        {
            Plan = paymentPlanmodel,
            Review = reviewModel ?? null,
            GigRequirements = gigRequirements.Select(x => new GigRequirementAnswerModel
            {
                Answer = x.Answer,
                Question = x.GigRequirement.Question,
            }).ToList(),
            Revision = revisionModels ??  null,           
            CurrentStatus = order.Status,
            Steps = steps,
            GigId = order.GigId.Value,
            GigTitle = order.Gig?.Title ?? string.Empty,
            GigImageUrl = order.Gig?.ImageUrls != null ? order.Gig?.ImageUrls.FirstOrDefault() : string.Empty,
            OrderNumber = order.OrderNumber,
            ConversationId = order.ConversationId,
            Delivery = deliveryModel ?? null
        };

        return orderDetailsModel;
    }

    private string SplitPascalCase(string input)
    {
        return Regex.Replace(input, @"(?<=[a-z])(?=[A-Z])", " ");
    }
}
