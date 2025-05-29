using Carter;
using MediatR;
using QuickHire.Application.Admin.Filters.CountriesFilter;
using QuickHire.Application.Admin.Filters.ModerationStatusFilter;
using QuickHire.Application.Admin.Filters.PriceFilter;
using QuickHire.Application.Admin.Filters.RoleFilter;
using QuickHire.Application.Admin.Models.Filters;

namespace QuickHire.Api.Modules.Shared;

public class FilterModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region Filters
        app.MapGet("admin/filters/moderation-status", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new ModerationStatusQuery());
            return Results.Ok(result);
        })
        .WithName("GetModerationStatus")
        .WithTags("Filters")
        .Produces<FilterItemModel[]>(StatusCodes.Status200OK)
        .WithDescription("Gets the moderation status for filtering.");

        app.MapGet("/admin/filters/price-range", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PriceFilterQuery());
            return Results.Ok(result);
        })
        .WithName("GetPriceRange")
        .WithTags("Filters")
        .Produces<FilterItemModel[]>(StatusCodes.Status200OK)
        .WithDescription("Gets the price range for filtering.");

        app.MapGet("/admin/filters/countries", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new CountriesFilterQuery());
            return Results.Ok(result);
        })
         .WithName("GetCountries")
        .WithTags("Filters")
        .Produces<FilterItemModel[]>(StatusCodes.Status200OK)
        .WithDescription("Gets the countries for filtering.");

        app.MapGet("/admin/filters/roles", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new RoleFilterQuery());
            return Results.Ok(result);
        })
         .WithName("GetRoles")
         .WithTags("Filters")
         .Produces<FilterItemModel[]>(StatusCodes.Status200OK)
         .WithDescription("Gets the roles for filtering.");

        #endregion
    }
}
