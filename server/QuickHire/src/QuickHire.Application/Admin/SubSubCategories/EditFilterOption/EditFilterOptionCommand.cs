using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.EditFilterOption;

public record EditFilterOptionCommand(int Id, string Name) : ICommand<Unit>;
