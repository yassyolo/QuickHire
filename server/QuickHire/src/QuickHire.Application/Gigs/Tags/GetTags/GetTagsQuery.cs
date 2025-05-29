using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Tags;

namespace QuickHire.Application.Gigs.Tags.GetTags;

public record GetTagsQuery(int? GigId, int? MainCategoryId) : IQuery<List<GetTagsResponseModel>>;

