using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.ProjectBriefs.AddProjectBrief;
public record AddProjectBriefCommand(string AboutBuyer, string Description, int SubSubCategoryId, int DeliveryDays, decimal Budget) : ICommand<Unit>;

