using MediatR;
using Microsoft.AspNetCore.Http;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.EditSubCategory;

public record EditSubCategoryCommand(int Id, string Name, IFormFile? Image = null) : ICommand<Unit>;

