using MediatR;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubCategories.AddSubCategory;

public class AddSubCategoryCommandHandler : ICommandHandler<AddSubCategoryCommand, AddSubCategoryReturnModel>
{
    private readonly IRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;

    public AddSubCategoryCommandHandler(IRepository repository, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<AddSubCategoryReturnModel> Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var mainCategory = await _repository.GetByIdAsync<MainCategory, int>(request.MainCategoryId);
        if (mainCategory == null)
        {
            throw new NotFoundException(nameof(MainCategory), request.MainCategoryId);
        }

        var subCategory = new SubCategory
        {
            Name = request.Name,
            MainCategoryId = request.MainCategoryId,
            Clicks = 0,
            CreatedOn = DateTime.Now,
        };

        var imagePath = _cloudinaryService.UploadFile(request.Image);
        if (imagePath == null)
        {
            throw new BadRequestException("Image upload failed", "Image upload failed.");
        }

        subCategory.ImageUrl = imagePath;

        await _repository.AddAsync(subCategory);
        await _repository.SaveChangesAsync();

        return new AddSubCategoryReturnModel
        {
            Id = subCategory.Id,
            ImageUrl = subCategory.ImageUrl,
        };
    }
}

