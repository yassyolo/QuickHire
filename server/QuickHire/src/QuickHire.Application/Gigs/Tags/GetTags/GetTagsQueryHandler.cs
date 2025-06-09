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

    //Todo
    public async Task<List<GetTagsResponseModel>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        /*var gigQueryable = _repository.GetAllReadOnly<Gig>();
        gigQueryable = _repository.GetAllIncluding<Gig>(x => x.SubSubCategory.SubCategory);
        if (request.GigId.HasValue)
        {
            gigQueryable = gigQueryable.Where(x => x.Id == request.GigId.Value);
        }

        if (request.MainCategoryId.HasValue)
        {
            gigQueryable = gigQueryable.Where(x => x.SubSubCategory.SubCategory.MainCategoryId == request.MainCategoryId.Value);
        }

        var gig = await _repository.FirstOrDefaultAsync(gigQueryable);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), "Entity not found");
        }

        var tags = _repository.GetAllReadOnly<Tag>().Where(x => x.GigId == gig.Id);
        var tagsList = await _repository.ToListAsync(tags);

        return tags.Adapt<GetTagsResponseModel>();*/

        return new List<GetTagsResponseModel>
        {
            new GetTagsResponseModel
            {
                Label = "Tag 1",
            },
            new GetTagsResponseModel
            {
                Label = "Tag 2",
            },
            new GetTagsResponseModel
            {
                Label = "Tag 3",
            },
            new GetTagsResponseModel
            {
                Label = "Tag 4",
            },
            new GetTagsResponseModel
            {
                Label = "Tag 5",
            },
        };
    }
}
