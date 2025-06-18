using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Tags;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Gigs.Tags.GetTags;

public class GetTagsQueryHandler : IQueryHandler<GetTagsQuery, List<GetTagsResponseModel>>
{
    private readonly IRepository _repository;

    public GetTagsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetTagsResponseModel>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        if (request.GigId.HasValue)
        {
            var tags = _repository.GetAllReadOnly<Tag>().Where(x => x.GigId == request.GigId.Value);
            var tagsList = await _repository.ToListAsync(tags);

            return tags.Select(x => new GetTagsResponseModel
            {
                Label = x.Name,
            }).ToList();
        }

        if (request.MainCategoryId.HasValue)
        {
            var gigQueryable = _repository.GetAllIncluding<Gig>(x => x.SubSubCategory.SubCategory.MainCategory).Where(x => x.SubSubCategory.SubCategory.MainCategoryId == request.MainCategoryId.Value);
            var gigs = await _repository.ToListAsync(gigQueryable);
            var gigIds = gigs.Select(x => x.Id).ToList();
            var tags = _repository.GetAllIncluding<Tag>(x => x.Gig.Orders).Where(x => gigIds.Contains(x.GigId));
            var tagsList = await _repository.ToListAsync(tags);
            tagsList = tagsList.OrderByDescending(x => x.Gig.Orders.Count()).ThenByDescending(x => x.Gig.Clicks).DistinctBy(x => x.Name).ToList(); 
            return tagsList.Select(x => new GetTagsResponseModel
            {
                Label = x.Name,
            }).ToList();
        }
        return new List<GetTagsResponseModel>();
        
    }
}
