using Carter;
using MediatR;
using QuickHire.Application.Admin.Models.Users.Notifications;
using QuickHire.Application.Users.Notifications.GetNotifications;
using QuickHire.Application.Users.Notifications.MarkAsRead;

namespace QuickHire.Api.Modules.Users;

public class UserModule : CarterModule
{
    //notifications
    //notifications/{id}/mark-as-read
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/notifications", async ([AsParameters] GetNotificationsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetNotifications")
            .WithTags("Users")
            .Produces<IEnumerable<GetNotificationsResponseModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get notifications for the current user");

        app.MapPost("/notifications/{Id}/mark-as-read", async (int Id, IMediator mediator) =>
        {
            var command = new MarkAsReadQuery(Id);
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("MarkNotificationAsRead")
            .WithTags("Users")
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Marks a notification as read by Id.");
    } 
}
