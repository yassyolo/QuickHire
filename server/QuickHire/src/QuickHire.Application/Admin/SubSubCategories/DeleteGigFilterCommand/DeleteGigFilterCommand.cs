using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.DeleteGigFilterCommand;

public record DeleteGigFilterCommand(int Id, string Reason) : ICommand<Unit>;
