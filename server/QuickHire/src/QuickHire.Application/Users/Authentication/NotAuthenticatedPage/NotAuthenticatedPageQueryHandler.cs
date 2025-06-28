using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Users.Models.NotAuthenticated;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Gigs;
using System.Linq;

namespace QuickHire.Application.Users.Authentication.NotAuthenticatedPage;

public class NotAuthenticatedPageQueryHandler : IQueryHandler<NotAuthenticatedPageQuery, NotAuthenticatedPageModel>
{
    private readonly IRepository _repository;

    public NotAuthenticatedPageQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotAuthenticatedPageModel> Handle(NotAuthenticatedPageQuery request, CancellationToken cancellationToken)
    {
        var subSubCategoryQuery = _repository.GetAllIncluding<SubSubCategory>(x => x.SubCategory);
        var subSubCategories = await _repository.ToListAsync(subSubCategoryQuery);
        var hotSubSubCategories = subSubCategories
            .Where(x => x.SubCategory?.ImageUrl != null)
.OrderByDescending(x => (int)(x.Gigs != null ? x.Gigs.Count() : 0))
            .ThenByDescending(x => x.Clicks)
            .ToList();

        var groupedPopularServices = hotSubSubCategories.GroupBy(x => x.SubCategoryId).Select(x => x.First()) 
            .Select(x => new PopularServicesModel
            {
                Id = x.Id,
                Title = x.Name,
                ImageUrl = x.SubCategory.ImageUrl
            }).Take(7).ToArray();

        var tagsQuery = _repository.GetAllIncluding<Tag>(x => x.Gig.Orders);
        var tags = await _repository.ToListAsync(tagsQuery);
        var popularTags = tags.OrderByDescending(x => x.Gig.Orders.Count()).ThenByDescending(x => x.Gig.Clicks).Take(4).Select(x => x.Name).ToArray();

        return new NotAuthenticatedPageModel
        {
            PopularServices = groupedPopularServices,
            PopularTags = popularTags
        };
    }
}
