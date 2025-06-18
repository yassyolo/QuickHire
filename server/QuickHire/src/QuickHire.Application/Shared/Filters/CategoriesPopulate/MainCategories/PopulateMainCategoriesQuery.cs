using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.Filters.CategoriesPopulate.MainCategories;

public record PopulateMainCategoriesQuery : IQuery<PopulateMainCategoriesModel[]>;
