using MediatR;
using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.SubSubCategoryDetails;

public record SubSubCategoryDetailsQuery(int Id) : IQuery<SubSubCategoryDetailsModel>;
