using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.DeleteSubCategory;

public record DeleteSubCategoryQuery(int Id, string Reason) : IQuery<Unit>;
