﻿using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.MainCategories.EditMainCategory;

public class EditMainCategoryCommandHandler : ICommandHandler<EditMainCategoryCommand, Unit>
{
    private readonly IRepository _repository;

    public EditMainCategoryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(EditMainCategoryCommand request, CancellationToken cancellationToken)
    {
        var mainCategory = await _repository.GetByIdAsync<MainCategory, int>(request.Id);

        if (mainCategory == null)
        {
            throw new NotFoundException(nameof(MainCategory), request.Id);
        }

        mainCategory.Name = request.Name;
        mainCategory.Description = request.Description;

        await _repository.UpdateAsync(mainCategory);

        return Unit.Value;
    }
}

