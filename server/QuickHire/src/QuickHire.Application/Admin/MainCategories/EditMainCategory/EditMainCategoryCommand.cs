using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.MainCategories.EditMainCategory;

public record EditMainCategoryCommand(int Id, string Name, string Description) : ICommand<Unit>;

