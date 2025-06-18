using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.ProjectBriefs.AddProjectBrief;
public record AddProjectBriefCommand(string AboutBuyer, string Description, int SubSubCategoryId, int DeliveryDays, decimal Budget) : ICommand<Unit>;

