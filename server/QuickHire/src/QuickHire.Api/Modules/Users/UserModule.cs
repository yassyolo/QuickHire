using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.Models.Users.Notifications;
using QuickHire.Application.Users.BillingsAndFinancialDocuments.AddBillingInfo;
using QuickHire.Application.Users.BillingsAndFinancialDocuments.EditBillingInfo;
using QuickHire.Application.Users.BillingsAndFinancialDocuments.GetBillingInfo;
using QuickHire.Application.Users.BillingsAndFinancialDocuments.GetFinancialDocuments;
using QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;
using QuickHire.Application.Users.Notifications.GetNotifications;
using QuickHire.Application.Users.Notifications.MarkAsRead;

namespace QuickHire.Api.Modules.Users;

public class UserModule : CarterModule
{
    //notifications
    //notifications/{id}/mark-as-read
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region Notifications
        app.MapGet("/notifications", async ([AsParameters] GetNotificationsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetNotifications")
            .WithTags("Notifications")
            .WithDescription("Get notifications for the current user");

        app.MapPut("/notifications/{Id}", async([AsParameters]MarkAsReadQuery query, IMediator mediator) =>
        {
            await mediator.Send(query);
            return Results.NoContent();
        })
            .WithName("MarkNotificationAsRead")
            .WithTags("Notifications")
            .WithDescription("Marks a notification as read by Id.");
        #endregion

        #region BillingsAndPayments
        //users/billings-and-payments/add
        app.MapPost("/users/billings-and-payments", async ([FromBody] AddBillingInfoCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("AddBillingInfo")
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem()
            .WithDescription("Adds a new billing and payment method for the user.");

        app.MapPut("/users/billings-and-payments", async ([FromBody] EditBillingInfoCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("UpdateBillingInfo")
            .WithTags("Users")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem()
            .WithDescription("Updates an existing billing and payment method for the user.");

        app.MapGet("/users/billings-and-payments", async ([AsParameters] GetBillingInfoQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetBillingInfo")
            .WithTags("Users")
            .Produces<GetBillingInfoModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Retrieves the billing and payment information for the user.");

        app.MapGet("/users/billings-and-payments/financial-documents", async([AsParameters] GetFinancialDocumentsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetFinancialDocuments")
            .WithTags("Users")
            .Produces<IEnumerable<FinancialDocumentRowModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Retrieves the financial documents for the user.");
        #endregion

    }
}
