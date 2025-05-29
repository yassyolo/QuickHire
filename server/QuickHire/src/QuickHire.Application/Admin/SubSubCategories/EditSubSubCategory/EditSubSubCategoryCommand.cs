using MediatR;
using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.EditSubSubCategory;

public record EditSubSubCategoryCommand(int Id, string Name) : ICommand<Unit>;

