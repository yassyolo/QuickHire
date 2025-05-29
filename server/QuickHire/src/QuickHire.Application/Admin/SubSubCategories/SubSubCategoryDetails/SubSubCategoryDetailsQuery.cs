using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.SubSubCategoryDetails;

public record SubSubCategoryDetailsQuery(int Id) : IQuery<Unit>;
