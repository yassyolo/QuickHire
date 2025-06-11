using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.Gigs.DeactivateGigAdmin;
using QuickHire.Application.Admin.Gigs.SearchGigsForAdmin;
using QuickHire.Application.Admin.Gigs.SellerForGig;
using QuickHire.Application.Admin.MainCategories.AddMainCategory;
using QuickHire.Application.Admin.MainCategories.DeleteMainCategory;
using QuickHire.Application.Admin.MainCategories.EditMainCategory;
using QuickHire.Application.Admin.MainCategories.GetMainCategoryForDelete;
using QuickHire.Application.Admin.MainCategories.MainCategoriesForLinks;
using QuickHire.Application.Admin.MainCategories.MainCategoryDetails;
using QuickHire.Application.Admin.MainCategories.MainCategoryPageDetails;
using QuickHire.Application.Admin.MainCategories.PopulateMainCategories;
using QuickHire.Application.Admin.MainCategories.SearchMainCategories;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Admin.Reporting.Report;
using QuickHire.Application.Admin.Reporting.ReportTables;
using QuickHire.Application.Admin.SubCategories.AddSubCategory;
using QuickHire.Application.Admin.SubCategories.DeleteSubCategory;
using QuickHire.Application.Admin.SubCategories.EditSubCategory;
using QuickHire.Application.Admin.SubCategories.GetSubCategoryForDelete;
using QuickHire.Application.Admin.SubCategories.PopularSubcategories;
using QuickHire.Application.Admin.SubCategories.PopulateSubCategories;
using QuickHire.Application.Admin.SubCategories.SearchSubCategories;
using QuickHire.Application.Admin.SubCategories.SubCategoriesHeader;
using QuickHire.Application.Admin.SubCategories.SubCategoriesInMainCategory;
using QuickHire.Application.Admin.SubCategories.SubCategoryDetails;
using QuickHire.Application.Admin.SubSubCategories.AddSubSubCategory;
using QuickHire.Application.Admin.SubSubCategories.DeleteFilterOption;
using QuickHire.Application.Admin.SubSubCategories.DeleteGigFilterCommand;
using QuickHire.Application.Admin.SubSubCategories.DeleteSubSubCategory;
using QuickHire.Application.Admin.SubSubCategories.EditFilter;
using QuickHire.Application.Admin.SubSubCategories.EditFilterOption;
using QuickHire.Application.Admin.SubSubCategories.EditSubSubCategory;
using QuickHire.Application.Admin.SubSubCategories.GetGigFilterForDelete;
using QuickHire.Application.Admin.SubSubCategories.GetSubSubCategoryForDelete;
using QuickHire.Application.Admin.SubSubCategories.PopulateSubSubCategories;
using QuickHire.Application.Admin.SubSubCategories.SearchSubSubCategories;
using QuickHire.Application.Admin.SubSubCategories.SubSubCategoryDetails;
using QuickHire.Application.Admin.Users.DeactivateUser;
using QuickHire.Application.Admin.Users.GetGigsForUser;
using QuickHire.Application.Admin.Users.SearchUsers;

namespace QuickHire.Api.Modules.Admin;

public class AdminModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region Main Categories

        app.MapGet("/admin/main-categories", async ([AsParameters] SearchMainCategoriesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("SearchMainCategories")
        .WithTags("Main Categories")  
        .WithDescription("Searches through main categories by id and keyword and returns paginated result");

        app.MapPost("/admin/main-categories/add", async ([FromBody] AddMainCategoryCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("AddMainCategory")
        .WithTags("Main Categories")        
        .WithDescription("Adds a new main category.");

        app.MapGet("/admin/main-categories/{id}", async ([AsParameters] MainCategoryDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("GetMainCategoryById")
        .WithTags("Main Categories")
        .WithDescription("Gets a main category by Id.");

        app.MapPut("/admin/main-categories/edit", async ([FromBody] EditMainCategoryCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
         .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("EditMainCategory")
        .WithTags("Main Categories")
        .WithDescription("Edits an existing main category by Id.");

        app.MapDelete("/admin/main-categories/delete", async ([FromBody] DeleteMainCategoryCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("DeleteMainCategory")
        .WithTags("Main Categories")
        .WithDescription("Deletes a main category by Id.");

        app.MapGet("/admin/main-categories/delete/{Id}", async ([AsParameters] GetMainCategoryForDeleteQuery command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
         .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("GetMainCategoryForDelete")
        .WithTags("Main Categories")
        .WithDescription("Gets a main category for deletion by Id.");

        app.MapGet("admin/main-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateMainCategoriesQuery());
            return Results.Ok(result);
        })
         .RequireAuthorization(new AuthorizeAttribute { Roles = "admin, seller" })
         .WithName("PopulateMainCategories")
         .WithTags("Main Categories")
         .WithDescription("Populates main categories.");

        #endregion

        #region Sub Categories

        app.MapGet("/admin/sub-categories", async ([AsParameters] SearchSubCategoriesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("SearchSubCategories")
        .WithTags("Sub Categories")       
        .WithDescription("Searches through sub categories by id and keyword and main category and returns paginated result");

        app.MapPut("/admin/sub-categories", async ([FromForm] EditSubCategoryCommand command, IMediator mediator) =>
        {
           var result = await mediator.Send(command);
            return Results.Ok(result);
        })
       .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
       .WithName("EditSubCategory")
       .WithTags("Sub Categories")
       .DisableAntiforgery()       
       .WithDescription("Edits an existing sub category by Id.");

        app.MapGet("/admin/sub-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateSubCategoriesQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("PopulateSubCategories")
        .WithTags("Sub Categories")
        .WithDescription("Populates sub categories.");

        app.MapPost("/admin/sub-categories/add", async ([FromForm] AddSubCategoryCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
       .WithName("AddSubCategory")
       .WithTags("Sub Categories")
       .WithDescription("Adds a new sub category.")
       .DisableAntiforgery();

        app.MapDelete("/admin/sub-categories", async ([FromBody] DeleteSubCategoryQuery query, IMediator mediator) =>
        {
            await mediator.Send(query);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
       .WithName("DeleteSubCategory")
       .WithTags("Sub Categories")
       .WithDescription("Deletes a sub category by Id.");

        app.MapGet("/admin/sub-categories/delete/{id}", async ([AsParameters] GetSubCategoryForDeleteQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("GetSubCategoryForDelete")
        .WithTags("Sub Categories")
        .WithDescription("Gets a sub category for deletion by Id.");

        app.MapGet("/admin/sub-categories/{id}", async ([AsParameters] SubCategoryDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
         .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
         .WithName("GetSubCategoryById")
         .WithDescription("Gets a sub category by Id.");
       

        #endregion

        #region Sub Sub Categories
        app.MapDelete("/admin/sub-sub-categories", async ([FromBody] DeleteSubSubCategoryCommand query, IMediator mediator) =>
        {
            await mediator.Send(query);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("DeleteSubSubCategory")
        .WithTags("Sub Sub Categories")       
        .WithDescription("Deletes a sub sub category by Id.");

        app.MapGet("/admin/sub-sub-categories/delete/{id}", async ([AsParameters] GetSubSubCategoryForDeleteQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("GetSubSubCategoryForDelete")
        .WithTags("Sub Sub Categories")
        .WithDescription("Gets a sub sub category for deletion by Id.");

        app.MapGet("/admin/sub-sub-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateSubSubCategoriesQuery());
            return Results.Ok(result);
        })
        .WithName("PopulateSubSubCategories")
        .WithTags("Sub Sub Categories")
        //.RequireAuthorization(new AuthorizeAttribute { Roles = "admin,buyer" })
        .WithDescription("Populates sub sub categories.");

        app.MapPost("/admin/sub-sub-categories/add", async ([FromBody] AddSubSubCategoryCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("AddSubSubCategory")
        .WithTags("Sub Sub Categories")
        .WithDescription("Adds a new sub sub category.");

        app.MapGet("/admin/sub-sub-categories", async([AsParameters] SearchSubSubCategoriesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("SearchSubSubCategories")
        .WithTags("Sub Sub Categories")
        .WithDescription("Searches through sub sub categories by id and keyword and main category and returns paginated result");
     
        app.MapPut("/admin/sub-sub-categories/edit", async([FromForm] EditSubSubCategoryCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("EditSubSubCategory")
        .WithTags("Sub Sub Categories")       
        .DisableAntiforgery()
        .WithDescription("Edits an existing sub sub category by Id.");

        app.MapGet("/admin/sub-sub-categories/{id}", async ([AsParameters] SubSubCategoryDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("GetSubSubCategoryById")
        .WithTags("Sub Sub Categories")
        .WithDescription("Gets a sub sub category by Id.");

        #region Gig Filters
        app.MapGet("/admin/sub-sub-categories/filters/delete/{Id}", async ([AsParameters] GetGigFilterForDeleteQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
       .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
       .WithName("GetSubSubCategoryFilterForDelete")
       .WithTags("Gig Filters")
       .WithDescription("Gets a filter for deletion by Id.");

        app.MapPut("/admin/sub-sub-categories/filters", async ([FromForm] EditFilterCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("EditSubSubCategoryFilters")
        .WithTags("Gig Filters")
        .DisableAntiforgery()
        .WithDescription("Edits filters for an existing sub sub category by Id.");

        app.MapDelete("/admin/sub-sub-categories/filters/delete", async ([FromBody] DeleteGigFilterCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("DeleteSubSubCategoryFilter")
        .WithTags("Gig Filters")
        .WithDescription("Deletes a filter for an existing sub sub category by Id.");

        #endregion      

        #region Filter Options

        app.MapDelete("/admin/sub-sub-categories/filters/options/delete/{Id}", async([FromForm] DeleteFilterOptionCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("GetSubSubCategoryFilterOptionForDelete")
        .WithTags("Filter Options")
        .DisableAntiforgery()
        .WithDescription("Gets a filter option for deletion by Id.");

        app.MapPut("/admin/sub-sub-categories/filters/options", async ([FromForm] EditFilterOptionCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("EditSubSubCategoryFilterOptions")
        .WithTags("Filter Options")           
        .DisableAntiforgery()
        .WithDescription("Edits filter options for an existing sub sub category by Id.");

        #endregion

        #endregion

        #region Users
        app.MapGet("/admin/users", async ([AsParameters] SearchUsersQuery query, IMediator mediator) => {
        var result = await mediator.Send(query);
        return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("SearchUsers")
        .WithTags("Users")
        .WithDescription("Searches through users by id and keyword and returns paginated result");

        app.MapPost("/admin/users/deactivate", async ([FromBody] DeactivateUserCommand query, IMediator mediator) => {
            var result = await mediator.Send(query);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("DeactivateUser")
        .WithTags("Users")
        .WithDescription("Deactivates a user by Id.");

        app.MapGet("/admin/users/gigs", async ([AsParameters] GetGigsForUserQuery query, IMediator mediator) => {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("GetGigsForUser")
        .WithTags("Users")
        .WithDescription("Gets gigs for a user by Id and returns paginated result");

        #endregion

        #region Gigs
        app.MapGet("/admin/gigs/seller", async ([AsParameters] GetSellerForGigQuery query, IMediator mediator) => {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
         .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
         .WithName("GetSellerForGig")
         .WithTags("Admin Gigs")
         .WithDescription("Gets seller for a gig by Id.");

        app.MapGet("/admin/gigs", async ([AsParameters] SearchGigsForAdminQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetGigsForAdmin")
        .WithTags("Admin Gigs")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithDescription("Gets gigs for admin and returns paginated result");

        app.MapPost("/admin/gigs/deactivate", async ([AsParameters] DeactivateGigAdminCommand query, IMediator mediator) => {
            var result = await mediator.Send(query);
            return Results.NoContent();
        })
        .WithName("DeactivateGig")
         .WithTags("Admin Gigs")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithDescription("Deactivates a gig by Id.");
        #endregion

        #region Reports

        app.MapGet("/admin/report", async ([AsParameters] GetReportTablesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "admin" })
        .WithName("GetReport")
        .WithTags("Report")
        .WithDescription("Gets report details by Id.");
        #endregion
    }

}
