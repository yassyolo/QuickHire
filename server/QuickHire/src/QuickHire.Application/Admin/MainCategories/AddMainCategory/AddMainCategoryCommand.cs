using MediatR;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.MainCategories.AddMainCategory;

public record AddMainCategoryCommand(string Name , string Description, List<FAQRequestModel> Faqs) : ICommand<Unit>;

