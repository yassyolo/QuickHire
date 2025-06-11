using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.FAQ.AddFAQ;
using QuickHire.Application.Admin.FAQ.DeleteFAQ;
using QuickHire.Application.Admin.FAQ.EditFAQ;
using QuickHire.Application.Admin.FAQ.GetFAQ;
using QuickHire.Application.Admin.Filters.CountriesFilter;
using QuickHire.Application.Admin.Filters.ModerationStatusFilter;
using QuickHire.Application.Admin.Filters.OrderStatusFilter;
using QuickHire.Application.Admin.Filters.PriceFilter;
using QuickHire.Application.Admin.Filters.RoleFilter;
using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Application.Admin.Models.Filters;

namespace QuickHire.Api.Modules.Shared;

public class SharedModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region FAQs 
        app.MapPost("/faqs", async([FromBody] AddFAQCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "seller, admin" })
        .WithName("AddFAQ")
        .WithTags("FAQs")
        .WithDescription("Adds a new FAQ for a gig or main category.");

        app.MapPut("/faqs", async ([FromForm] EditFAQCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin, seller" })
        .WithName("EditFAQ")
        .WithTags("FAQs")
        .DisableAntiforgery()
        .WithDescription("Edits an existing FAQ by Id.");

        app.MapGet("/faqs", async ([AsParameters] GetFAQQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer, seller, admin" })
        .WithName("SearchFAQs")
        .WithTags("FAQs")
        .WithDescription("Searches through FAQs by gig, main category or user id.");

        app.MapDelete("/faqs", async ([FromBody] DeleteFAQCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "seller, admin" })
        .WithName("DeleteFAQ")
        .WithTags("FAQs")
        .WithDescription("Deletes a FAQ by Id.");

        #endregion

        #region Filters
        app.MapGet("admin/filters/moderation-status", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new ModerationStatusQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "seller, admin" })
        .WithName("GetModerationStatus")
        .WithTags("Filters")
        .WithDescription("Gets the moderation status for filtering.");

        app.MapGet("/admin/filters/price-range", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PriceFilterQuery());
            return Results.Ok(result);
        })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetPriceRange")
        .WithTags("Filters")
        .WithDescription("Gets the price range for filtering.");

        app.MapGet("/admin/filters/countries", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new CountriesFilterQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer, admin" })
        .WithName("GetCountries")
        .WithTags("Filters")
        .WithDescription("Gets the countries for filtering.");

        app.MapGet("/admin/filters/role", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new RoleFilterQuery());
            return Results.Ok(result);
        })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
         .WithName("GetRoles")
         .WithTags("Filters")
         .WithDescription("Gets the roles for filtering.");

        app.MapGet("/admin/filters/order-status", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new OrderStatusQuery());
            return Results.Ok(result);
        })
         .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer, seller" })
         .WithName("GetOrderStatus")
         .WithTags("Filters")
         .WithDescription("Gets the order status for filtering.");

        #endregion
    }
}
