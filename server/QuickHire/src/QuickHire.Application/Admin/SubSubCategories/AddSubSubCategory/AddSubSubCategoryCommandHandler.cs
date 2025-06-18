using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.AddSubSubCategory;

public class AddSubSubCategoryCommandHandler : ICommandHandler<AddSubSubCategoryCommand, int>
{
    private readonly IRepository _repository;

    public AddSubSubCategoryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(AddSubSubCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var subsubCategory = new Domain.Categories.SubSubCategory
            {
                Name = request.Name,
                SubCategoryId = request.SubCategoryId,
                CreatedOn = DateTime.Now,
            };

            await _repository.AddAsync(subsubCategory);
            await _repository.SaveChangesAsync();

            foreach (var filter in request.Filters)
            {
                var subSubCategoryFilter = new Domain.Categories.GigFilter
                {
                    SubSubCategoryId = subsubCategory.Id,
                    Title = filter.Name,
                    Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes
                };

                await _repository.AddAsync(subSubCategoryFilter);
                await _repository.SaveChangesAsync();

                foreach (var value in filter.Options)
                {
                    var option = new Domain.Categories.FilterOption
                    {
                        Name = value,
                        GigFilterId = subSubCategoryFilter.Id
                    };
                    await _repository.AddAsync(option);
                }
            }

            await _repository.SaveChangesAsync();

            return subsubCategory.Id;
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while adding the sub-sub-category.", ex.Message);
        }        
    }
}
