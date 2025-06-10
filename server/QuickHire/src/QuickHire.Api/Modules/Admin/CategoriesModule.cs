using Carter;
using CloudinaryDotNet;
using MediatR;
using Microsoft.AspNetCore.Builder;
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
using QuickHire.Application.Admin.Models.MainCategories;
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
using QuickHire.Application.Admin.SubSubCategories.DeleteGigFilterCommand;
using QuickHire.Application.Admin.SubSubCategories.DeleteSubSubCategory;
using QuickHire.Application.Admin.SubSubCategories.EditFilter;
using QuickHire.Application.Admin.SubSubCategories.EditFilterOption;
using QuickHire.Application.Admin.SubSubCategories.EditSubSubCategory;
using QuickHire.Application.Admin.SubSubCategories.GetFilterOptionForDelete;
using QuickHire.Application.Admin.SubSubCategories.GetGigFilterForDelete;
using QuickHire.Application.Admin.SubSubCategories.GetSubSubCategoryForDelete;
using QuickHire.Application.Admin.SubSubCategories.PopulateSubSubCategories;
using QuickHire.Application.Admin.SubSubCategories.SearchSubSubCategories;
using QuickHire.Application.Admin.SubSubCategories.SubSubCategoryDetails;
using QuickHire.Application.Admin.Users.DeactivateUser;
using QuickHire.Application.Admin.Users.GetGigsForUser;
using QuickHire.Application.Admin.Users.SearchUsers;
using System;

namespace QuickHire.Api.Modules.Admin;

public class CategoriesModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region Main Categories

        app.MapGet("/admin/main-categories", async ([AsParameters] SearchMainCategoriesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("SearchMainCategories")
        .WithTags("Main Categories")
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .WithDescription("Searches through main categories by id and keyword and returns paginated result");

        app.MapPost("/admin/main-categories/add", async ([FromBody] AddMainCategoryCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("AddMainCategory")
        .WithTags("Main Categories")
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem()
        .WithDescription("Adds a new main category.");

        app.MapGet("/admin/main-categories/{id}", async ([AsParameters] MainCategoryDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetMainCategoryById")
         .WithTags("Main Categories")
        .Produces<MainCategoryDetailsModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Gets a main category by Id.");

        app.MapPut("/admin/main-categories/edit", async ([FromBody] EditMainCategoryCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("EditMainCategory")
        .WithTags("Main Categories")
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Edits an existing main category by Id.");

        app.MapDelete("/admin/main-categories/delete", async ([FromBody] DeleteMainCategoryCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("DeleteMainCategory")
        .WithTags("Main Categories")
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem();

        app.MapGet("/admin/main-categories/delete/{Id}", async ([AsParameters] GetMainCategoryForDeleteQuery command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("GetMainCategoryForDelete")
        .WithTags("Main Categories")
        .Produces<MainCategoryDetailsModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Gets a main category for deletion by Id.");

        app.MapGet("admin/main-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateMainCategoriesQuery());
            return Results.Ok(result);
        })
         .WithName("PopulateMainCategories")
         .WithTags("Main Categories")
         .Produces<PopulateMainCategoriesModel[]>(StatusCodes.Status200OK)
         .WithDescription("Populates main categories.");

        app.MapGet("/main-categories/link", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new MainCategoryForLinksQuery());
            return Results.Ok(result);
        })
         .WithName("MainCategoriesForLinks")
         .WithTags("Main Categories")
         .Produces<IEnumerable<MainCategoryForLinksModel>>(StatusCodes.Status200OK)
         .WithDescription("Gets main categories for links in the frontend.");

        app.MapGet("/main-categories/page/{id}", async([AsParameters] MainCategoryPageDeatilsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("MainCategoryPageDetails")
            .WithTags("Main Categories")
            .Produces<MainCategoryPageDeatilsModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Gets main category page details by Id.");

        #endregion

        #region Sub Categories

        app.MapGet("/admin/sub-categories", async ([AsParameters] SearchSubCategoriesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("SearchSubCategories")
        .WithTags("Sub Categories")
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .WithDescription("Searches through sub categories by id and keyword and main category and returns paginated result");

        app.MapPut("/admin/sub-categories", async ([FromForm] EditSubCategoryCommand command, IMediator mediator) =>
        {
           var result = await mediator.Send(command);
            return Results.Ok(result);
        })
       .WithName("EditSubCategory")
       .WithTags("Sub Categories")
       .DisableAntiforgery()
       .Produces(StatusCodes.Status404NotFound)
       .ProducesValidationProblem()
       .WithDescription("Edits an existing sub category by Id.");

        app.MapGet("/admin/sub-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateSubCategoriesQuery());
            return Results.Ok(result);
        })
        .WithName("PopulateSubCategories")
        .WithTags("Sub Categories")
        .Produces<IEnumerable<PopulateSubCategoriesModel>>(StatusCodes.Status200OK)
        .WithDescription("Populates sub categories.");

        app.MapPost("/admin/sub-categories/add", async ([FromForm] AddSubCategoryCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
       .WithName("AddSubCategory")
       .WithTags("Sub Categories")
       .Produces<int>(StatusCodes.Status204NoContent)
       .Produces(StatusCodes.Status400BadRequest)
       .ProducesValidationProblem()
       .WithDescription("Adds a new sub category.")
       .DisableAntiforgery();

        app.MapDelete("/admin/sub-categories", async ([FromBody] DeleteSubCategoryQuery query, IMediator mediator) =>
        {
            await mediator.Send(query);
            return Results.NoContent();
        })
       .WithName("DeleteSubCategory")
       .WithTags("Sub Categories")
       .Produces<Unit>(StatusCodes.Status204NoContent)
       .Produces(StatusCodes.Status404NotFound)
       .Produces(StatusCodes.Status400BadRequest)
       .ProducesValidationProblem()
       .WithDescription("Deletes a sub category by Id.");

        app.MapGet("/admin/sub-categories/delete/{id}", async ([AsParameters] GetSubCategoryForDeleteQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetSubCategoryForDelete")
        .WithTags("Sub Categories")
        .Produces<GetSubCategoryForDeleteModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Gets a sub category for deletion by Id.");

        app.MapGet("/admin/sub-categories/{id}", async ([AsParameters] SubCategoryDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
          .WithName("GetSubCategoryById")
.Produces<SubCategoryDetailsModel>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.ProducesValidationProblem()
.WithDescription("Gets a sub category by Id.");

        app.MapGet("/admin/sub-categories/header/{id}", async([AsParameters] SubCategoriesHeaderQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSubCategoryHeader")
            .WithTags("Sub Categories")
            .Produces<SubCategoriesHeaderResponseModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Gets a sub category header by Id.");
        
        app.MapGet("/admin/sub-categories-in-main-category/{id}", async([AsParameters] SubCategoriesInMainCategoryQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("SubCategoriesInMainCategory")
            .WithTags("Sub Categories")
            .Produces<IEnumerable<SubCategoriesInMainCategoryResponseModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Gets sub categories in a main category by Id.");
        app.MapGet("/admin/sub-categories/popular/{id}", async([AsParameters] PopularSubcategoriesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("PopularSubCategoriesInMainCategory")
            .WithTags("Sub Categories")
            .Produces<IEnumerable<PopularSubCategoriesResponseModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Gets popular sub categories in a main category by Id.");



        #endregion

        #region Sub Sub Categories
        app.MapDelete("/admin/sub-sub-categories", async ([FromBody] DeleteSubSubCategoryCommand query, IMediator mediator) =>
        {
            await mediator.Send(query);
            return Results.NoContent();
        })
        .WithName("DeleteSubSubCategory")
        .WithTags("Sub Sub Categories")
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem()
        .WithDescription("Deletes a sub sub category by Id.");

        app.MapGet("/admin/sub-sub-categories/delete/{id}", async ([AsParameters] GetSubSubCategoryForDeleteQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetSubSubCategoryForDelete")
        .WithTags("Sub Sub Categories")
        .Produces<GetSubCategoryForDeleteModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Gets a sub sub category for deletion by Id.");

        app.MapGet("/admin/sub-sub-categories/populate", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new PopulateSubSubCategoriesQuery());
            return Results.Ok(result);
        })
        .WithName("PopulateSubSubCategories")
        .WithTags("Sub Sub Categories")
        .Produces<PopulateSubCategoriesModel[]>(StatusCodes.Status200OK)
        .WithDescription("Populates sub sub categories.");

        app.MapPost("/admin/sub-sub-categories/add", async ([FromBody] AddSubSubCategoryCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("AddSubSubCategory")
            .WithTags("Sub Sub Categories")
            .Produces<int>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem()
            .WithDescription("Adds a new sub sub category.");


        app.MapGet("/admin/sub-sub-categories", async([AsParameters] SearchSubSubCategoriesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("SearchSubSubCategories")
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .WithDescription("Searches through sub sub categories by id and keyword and main category and returns paginated result");

       
        app.MapPut("/admin/sub-sub-categories/edit", async([FromForm] EditSubSubCategoryCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("EditSubSubCategory")
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .DisableAntiforgery()
        .ProducesValidationProblem()
        .WithDescription("Edits an existing sub sub category by Id.");

        app.MapPut("/admin/sub-sub-categories/filters", async([FromForm] EditFilterCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("EditSubSubCategoryFilters")
            .WithTags("Sub Sub Categories")
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .DisableAntiforgery()
            .ProducesValidationProblem()
            .WithDescription("Edits filters for an existing sub sub category by Id.");

        app.MapDelete("/admin/sub-sub-categories/filters/delete", async ([FromBody] DeleteGigFilterCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("DeleteSubSubCategoryFilter")
            .WithTags("Sub Sub Categories")
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Deletes a filter for an existing sub sub category by Id.");

        app.MapGet("/admin/sub-sub-categories/{id}", async ([AsParameters] SubSubCategoryDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSubSubCategoryById")
            .WithTags("Sub Sub Categories")
            .Produces<SubSubCategoryDetailsModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Gets a sub sub category by Id.");
        app.MapGet("/admin/sub-sub-categories/filters/delete/{Id}", async([AsParameters] GetGigFilterForDeleteQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSubSubCategoryFilterForDelete")
            .WithTags("Sub Sub Categories")
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Gets a filter for deletion by Id.");

        app.MapPut("/admin/sub-sub-categories/filters/options", async ([FromBody] EditFilterOptionCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("EditSubSubCategoryFilterOptions")
            .WithTags("Sub Sub Categories")
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Edits filter options for an existing sub sub category by Id.");

        #endregion

        #region Users
        app.MapGet("/admin/users", async ([AsParameters] SearchUsersQuery query, IMediator mediator) => {
        var result = await mediator.Send(query);
        return Results.Ok(result);
        })
           
    .WithName("SearchUsers")
        .WithTags("Users")
        .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithDescription("Searches through users by id and keyword and returns paginated result");

        app.MapPost("/admin/users/deactivate", async ([FromBody] DeactivateUserCommand query, IMediator mediator) => {
            var result = await mediator.Send(query);
            return Results.NoContent();
        })
            .WithName("DeactivateUser")
            .WithTags("Users")
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Deactivates a user by Id.");

        app.MapGet("/admin/users/gigs", async ([AsParameters] GetGigsForUserQuery query, IMediator mediator) => {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetGigsForUser")
            .WithTags("Users")
            .ProducesValidationProblem()
            .WithDescription("Gets gigs for a user by Id and returns paginated result");
        app.MapGet("/admin/gigs/seller", async ([AsParameters] GetSellerForGigQuery query, IMediator mediator) => {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSellerForGig")
            .WithTags("Users")
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Gets seller for a gig by Id.");

        app.MapGet("/admin/gigs", async([AsParameters] SearchGigsForAdminQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetGigsForAdmin")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithDescription("Gets gigs for admin and returns paginated result");
        #endregion

       app.MapPost("/admin/gigs/deactivate", async ([AsParameters] DeactivateGigAdminCommand query, IMediator mediator) => {
            var result = await mediator.Send(query);
            return Results.NoContent();
        })
            .WithName("DeactivateGig")
            .WithTags("Gigs")
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Deactivates a gig by Id.");

        ///admin/report
        app.MapPost("/admin/report", async ([AsParameters] ReportItemCommand query, IMediator mediator) =>
        {
            await mediator.Send(query);
            return Results.NoContent();
        })
            .WithName("ReportItem")
            .WithTags("Report")
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Reports an item (gig, user, etc.) by Id.");

        app.MapGet("/admin/report", async ([AsParameters] GetReportTablesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetReport")
            .WithTags("Report")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithDescription("Gets report details by Id.");



    }

}
