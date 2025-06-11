using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.DeleteFilterOption;

public record DeleteFilterOptionCommand(int Id, string Reason) : ICommand<Unit>;
