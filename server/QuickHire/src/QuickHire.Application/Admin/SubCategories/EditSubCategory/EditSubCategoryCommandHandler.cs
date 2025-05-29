using Mapster;
using MediatR;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubCategories.EditSubCategory;

public class EditSubCategoryCommandHandler : ICommandHandler<EditSubCategoryCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;

    public EditSubCategoryCommandHandler(IRepository repository, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Unit> Handle(EditSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var subCategory = await _repository.GetByIdAsync<SubCategory, int>(request.Id);
        if (subCategory == null)
        {
            throw new NotFoundException(nameof(SubCategory), request.Id);
        }

        if(subCategory.Name != request.Name)
        {
            subCategory.Name = request.Name;
        }

        if(request.Image != null)
        {
            var imagePath = _cloudinaryService.UploadFile(request.Image);
            if (imagePath == null)
            {
                throw new BadRequestException("Image upload failed", "Image upload failed.");
            }

            subCategory.ImageUrl = imagePath;
        }

        await _repository.AddAsync(subCategory);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

