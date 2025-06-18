using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Admin.Reporting.ReportTables;
using QuickHire.Application.Shared.FAQ.AddFAQ;
using QuickHire.Application.Shared.FAQ.DeleteFAQ;
using QuickHire.Application.Shared.FAQ.EditFAQ;
using QuickHire.Application.Shared.FAQ.GetFAQ;
using QuickHire.Application.Shared.Filters.CategoriesPopulate.MainCategories;
using QuickHire.Application.Shared.Filters.CategoriesPopulate.SubCategories;
using QuickHire.Application.Shared.Filters.CategoriesPopulate.SubSubCategories;
using QuickHire.Application.Shared.Filters.CountriesFilter;
using QuickHire.Application.Shared.Filters.DeliveryTimeFilter;
using QuickHire.Application.Shared.Filters.ModerationStatusFilter;
using QuickHire.Application.Shared.Filters.OrderStatusFilter;
using QuickHire.Application.Shared.Filters.PopulateLanguages;
using QuickHire.Application.Shared.Filters.PriceFilter;
using QuickHire.Application.Shared.Filters.RoleFilter;
using QuickHire.Application.Shared.Filters.ServiceIncludesFilter;
using QuickHire.Application.Users.Authentication.NotAuthenticatedPage;

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
        .RequireAuthorization(new AuthorizeAttribute { Roles = "seller,admin" })
        .WithName("AddFAQ")
        .WithTags("FAQs")
        .WithDescription("Adds a new FAQ for a gig or main category.");

        app.MapPut("/faqs", async ([FromForm] EditFAQCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin,seller" })
        .WithName("EditFAQ")
        .WithTags("FAQs")
        .DisableAntiforgery()
        .WithDescription("Edits an existing FAQ by Id.");

        app.MapGet("/faqs", async ([AsParameters] GetFAQQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller,admin" })
        .WithName("SearchFAQs")
        .WithTags("FAQs")
        .WithDescription("Searches through FAQs by gig, main category or user id.");

        app.MapDelete("/faqs", async ([FromBody] DeleteFAQCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "seller,admin" })
        .WithName("DeleteFAQ")
        .WithTags("FAQs")
        .WithDescription("Deletes a FAQ by Id.");

        #endregion

        #region Filters
        app.MapGet("/gig-filters/populate", async ([AsParameters] ServiceIncludesFilterQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(
                result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller" })
        .WithName("PopulateGigFilters")
        .WithTags("Gig Filters")
        .WithDescription("Populates gig filters for sub sub categories.");

        app.MapGet("filters/moderation-status", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new ModerationStatusQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin,seller" })
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
        
        app.MapGet("/admin/filters/delivery-time", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new DeliveryTimeFilterQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetDeliveryTime")
        .WithTags("Filters")
        .WithDescription("Gets the delivery time for filtering.");
        
        app.MapGet("/admin/filters/countries", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new CountriesFilterQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,admin" })
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
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller" })
        .WithName("GetOrderStatus")
        .WithTags("Filters")
        .WithDescription("Gets the order status for filtering.");

        app.MapGet("/main-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateMainCategoriesQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "seller,admin" })
        .WithName("PopulateMainCategories")
        .WithTags("Filters")
        .WithDescription("Populates main categories.");

        app.MapGet("/sub-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateSubCategoriesQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("PopulateSubCategories")
        .WithTags("Filters")
        .WithDescription("Populates sub categories.");

        app.MapGet("/languages/populate", async ([AsParameters] PopulateLanguagesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "seller,buyer" })
        .WithName("PopulateLanguages")
        .WithTags("Seller")
        .WithDescription("Populate the languages for the seller's profile.");

        app.MapGet("/sub-sub-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateSubSubCategoriesQuery());
            return Results.Ok(result);
        })
        .WithName("PopulateSubSubCategories")
        .WithTags("Sub Sub Categories")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin,buyer,seller" })
        .WithDescription("Populates sub sub categories.");

        #endregion

        #region NotAuthenticated

        app.MapGet("/not-authenticated", async ([AsParameters] NotAuthenticatedPageQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
       .WithName("NotAuthenticatedPage")
       .WithTags("Not Authenticated")
       .WithDescription("Returns the not authenticated page content based on the provided query parameters.");
        #endregion
    }

}
