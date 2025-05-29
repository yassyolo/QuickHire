using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Orders.Models.Reviews;
using QuickHire.Application.Orders.Reviews.GetRatings;

namespace QuickHire.Application.Orders.Reviews.GetReview;

public class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, List<ReviewResponseRowModel>>
{
    private readonly IRepository _repository;

    public GetReviewsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ReviewResponseRowModel>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        /*var reviews = _repository.GetAllReadOnly<QuickHire.Domain.Orders.Review>()
            .Where(x => !string.IsNullOrEmpty(request.GigId) ? x.Order.GigId == request.GigId : x.Order.);
        if(string.IsNullOrEmpty)*/
        return new List<ReviewResponseRowModel>
        {
            new ReviewResponseRowModel
            {
                Rating = 5,
                Comment = "Great service!",
            },
        };
    }
}
