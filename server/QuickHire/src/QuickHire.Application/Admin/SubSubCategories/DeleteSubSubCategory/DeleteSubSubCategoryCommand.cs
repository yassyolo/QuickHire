using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.DeleteSubSubCategory;

public record DeleteSubSubCategoryCommand(int Id, string Reason) : ICommand<Unit>;

