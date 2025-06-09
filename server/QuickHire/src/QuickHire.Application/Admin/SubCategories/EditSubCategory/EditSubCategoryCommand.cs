using MediatR;
using Microsoft.AspNetCore.Http;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.EditSubCategory;

public record EditSubCategoryCommand : ICommand<AddSubCategoryReturnModel>
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public IFormFile? Image { get; init; }
}
