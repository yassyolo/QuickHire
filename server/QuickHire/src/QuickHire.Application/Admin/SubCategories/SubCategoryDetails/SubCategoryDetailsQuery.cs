using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.SubCategoryDetails;

public record SubCategoryDetailsQuery(int Id) : IQuery<SubCategoryDetailsModel>;

