using MediatR;
using Microsoft.AspNetCore.Http;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.AddSubCategory;

public record AddSubCategoryCommand(int MainCategoryId, string Name, IFormFile Image) : ICommand<AddSubCategoryReturnModel>;
