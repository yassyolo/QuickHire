using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Gigs.GigsInSubSubCategory;

public record GigsInSubSubCategoryQuery(int? SubCategoryId, string? Keyword, int? SubSubCategoryId, int? PriceRangeId, int? DeliveryTimeId, List<int>? CountryIds, List<int>? LanguageIds, List<int>? SelectedOptionIds, int CurrentPage, int ItemsPerPage) : IQuery<PaginatedResultModel<GigCardModel>>;
