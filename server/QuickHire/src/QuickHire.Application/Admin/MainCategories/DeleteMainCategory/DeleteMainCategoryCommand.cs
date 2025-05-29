using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.MainCategories.DeleteMainCategory;

public record DeleteMainCategoryCommand(int Id, string Reason) : ICommand<Unit>;

