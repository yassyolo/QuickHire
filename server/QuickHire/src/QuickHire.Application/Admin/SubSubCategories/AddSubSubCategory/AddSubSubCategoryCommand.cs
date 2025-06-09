using MediatR;
using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.AddSubSubCategory;

public record AddSubSubCategoryCommand(string Name, AddGigFiltersModel[] Filters, int SubCategoryId) : ICommand<Unit>;

