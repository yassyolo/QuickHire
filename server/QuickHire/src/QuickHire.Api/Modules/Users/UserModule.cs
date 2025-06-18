using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region Notifications
        app.MapGet("/notifications", async ([AsParameters] GetNotificationsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller" })
        .WithName("GetNotifications")
        .WithTags("Notifications")
        .WithDescription("Get notifications for the current user");

        app.MapPut("/notifications/{Id}", async([AsParameters]MarkAsReadQuery query, IMediator mediator) =>
        {
            await mediator.Send(query);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller" })
        .WithName("MarkNotificationAsRead")
        .WithTags("Notifications")
        .WithDescription("Marks a notification as read by Id.");
        #endregion

        #region BillingsAndPayments
        app.MapPost("/users/billings-and-payments", async ([FromBody] AddBillingInfoCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("AddBillingInfo")
        .WithTags("Users")
        .WithDescription("Adds a new billing and payment method for the user.");

        app.MapPut("/users/billings-and-payments", async ([FromBody] EditBillingInfoCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("UpdateBillingInfo")
        .WithTags("Users")
        .WithDescription("Updates an existing billing and payment method for the user.");

        app.MapGet("/users/billings-and-payments", async ([AsParameters] GetBillingInfoQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller" })
        .WithName("GetBillingInfo")
        .WithTags("Users")
        .WithDescription("Retrieves the billing and payment information for the user.");

        app.MapGet("/users/billings-and-payments/financial-documents", async([AsParameters] GetFinancialDocumentsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller" })
        .WithName("GetFinancialDocuments")
        .WithTags("Users")
        .WithDescription("Retrieves the financial documents for the user.");
        #endregion

    }
}
