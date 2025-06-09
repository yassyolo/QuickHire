using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using System.Windows.Input;

namespace QuickHire.Application.Admin.SubSubCategories.EditFilter;

public record EditFilterCommand(int Id, string Name) : ICommand<Unit>;

