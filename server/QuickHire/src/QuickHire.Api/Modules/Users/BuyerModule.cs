using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.MainCategories.MainCategoriesForLinks;
using QuickHire.Application.Admin.MainCategories.MainCategoryPageDetails;
using QuickHire.Application.Admin.Reporting.Report;
using QuickHire.Application.Admin.SubCategories.PopularSubcategories;
using QuickHire.Application.Admin.SubCategories.SubCategoriesHeader;
using QuickHire.Application.Admin.SubCategories.SubCategoriesInMainCategory;
using QuickHire.Application.Users.Authentication.ChangePassword;
using QuickHire.Application.Users.Buyer.AddBuyerDetails;
using QuickHire.Application.Users.Buyer.EditBuyerDetails;
using QuickHire.Application.Users.Buyer.Profile;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Application.Users.ProjectBriefs.AddProjectBrief;
using QuickHire.Application.Users.ProjectBriefs.BuyerProjectBriefs;
using QuickHire.Application.Users.ProjectBriefs.WithdrawProjectBrief;
using QuickHire.Application.Users.Seller.Profile.EditEducation;
using QuickHire.Application.Users.Seller.Profile.EditLanguages;

namespace QuickHire.Api.Modules.Users;

public class BuyerModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region Categories

        app.MapGet("/admin/sub-categories/popular/{id}", async ([AsParameters] PopularSubcategoriesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("PopularSubCategoriesInMainCategory")
        .WithTags("Sub Categories")
        .WithDescription("Gets popular sub categories in a main category by Id.");

        app.MapGet("/sub-categories/header/{id}", async ([AsParameters] SubCategoriesHeaderQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
.RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
.WithName("GetSubCategoryHeader")
.WithTags("Sub Categories")
.WithDescription("Gets a sub category header by Id.");


        app.MapGet("/main-categories/link", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new MainCategoryForLinksQuery());
            return Results.Ok(result);
        })
         .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller" })
         .WithName("MainCategoriesForLinks")
         .WithTags("Main Categories")
         .WithDescription("Gets main categories for links in the frontend.");

        app.MapGet("/main-categories/page/{id}", async ([AsParameters] MainCategoryPageDeatilsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
 .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
 .WithName("MainCategoryPageDetails")
 .WithTags("Main Categories")
 .WithDescription("Gets main category page details by Id.");

        app.MapGet("/admin/sub-categories-in-main-category/{id}", async ([AsParameters] SubCategoriesInMainCategoryQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
.RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
.WithName("SubCategoriesInMainCategory")
.WithTags("Sub Categories")
.WithDescription("Gets sub categories in a main category by Id.");
        #endregion
        #region Project Briefs
        app.MapPost("/buyers/project-briefs", async ([FromBody] AddProjectBriefCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("AddProjectBrief")
        .WithTags("Buyer Project Briefs")
        .WithDescription("Add a new project brief for the buyer.");

        app.MapGet("/buyers/project-briefs", async ([AsParameters] GetBuyerProjectBriefsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }
        )
            .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetBuyerProjectBriefs")
        .WithTags("Buyer Project Briefs")
        .WithDescription("Get all project briefs for the buyer.");

        app.MapDelete("/buyers/project-briefs/delete/{id}", async ([AsParameters] WithdrawProjectBriefCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
                    .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
    .WithName("DeleteProjectBrief")
    .WithDescription("Delete a project brief by ID.");
        #endregion


        #region Profile
        app.MapGet("/buyers/profile", async ([AsParameters] GetBuyerProfileQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
           .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
           .WithName("GetBuyerProfile")
           .WithTags("Profile")
           .WithDescription("Get the buyer's profile information.");

        app.MapPost("/buyers/profile", async ([FromForm] AddBuyerDetailsCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
               .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
               .WithTags("Profile")
               .DisableAntiforgery()
    .WithName("AddBuyerDetails")
    .WithDescription("Add or update the buyer's profile details.");

        app.MapPut("buyers/profile", async ([FromForm] EditBuyerCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
            .WithName("UpdateBuyerProfile")
            .DisableAntiforgery()
            .WithTags("Profile")
            .WithDescription("Update the buyer's profile information.");

        app.MapPut("user/languages", async ([FromBody] EditLanguagesCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
    .WithName("EditUserLanguages")
       .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer,seller" })
       .WithTags("Profile")
    .WithDescription("Edit the languages of the user.");
        #endregion   


        #region Reports
        app.MapPost("/admin/report", async ([AsParameters] ReportItemCommand query, IMediator mediator) =>
        {
            await mediator.Send(query);
            return Results.NoContent();
        })
.RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
.WithName("ReportItem")
.WithTags("Report")
.WithDescription("Reports an item (gig, user, etc.) by Id.");
        #endregion



    }
}

