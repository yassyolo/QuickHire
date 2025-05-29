using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.MainCategories.MainCategoryDetails;

public record MainCategoryDetailsQuery(int Id) : IQuery<MainCategoryDetailsModel>; 

