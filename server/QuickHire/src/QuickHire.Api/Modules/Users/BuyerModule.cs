using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.Reporting.Report;
using QuickHire.Application.CustomOffers.GetCustomOffer;
using QuickHire.Application.ProjectBriefs.AddProjectBrief;
using QuickHire.Application.ProjectBriefs.BuyerProjectBriefs;
using QuickHire.Application.ProjectBriefs.WithdrawProjectBrief;
using QuickHire.Application.Users.Authentication.ChangePassword;
using QuickHire.Application.Users.Authentication.UpdateProfile;
using QuickHire.Application.Users.Buyer.BuyerProfile.AddBuyerDetails;
using QuickHire.Application.Users.Buyer.BuyerProfile.EditBuyerDetails;
using QuickHire.Application.Users.Buyer.BuyerProfile.Profile;
using QuickHire.Application.Users.Buyer.Categories.Header.MainCategoriesForLinks;
using QuickHire.Application.Users.Buyer.Categories.Header.SubCategoriesHeader;
using QuickHire.Application.Users.Buyer.Categories.MainCategoryDetails.MainCategoryPageDetails;
using QuickHire.Application.Users.Buyer.Categories.MainCategoryDetails.SubCategoriesInMainCategory;
using QuickHire.Application.Users.Buyer.Categories.SubCategories;
using QuickHire.Application.Users.Buyer.Categories.SubCategoryPage;
using QuickHire.Application.Users.Buyer.Categories.SubSubCategoriesInSubCategory;
using QuickHire.Application.Users.Buyer.Categories.SubSubCategoryPageData;
using QuickHire.Application.Users.Buyer.FirstPage.ExploreHotGigs;
using QuickHire.Application.Users.Buyer.FirstPage.HotGigsInMainCategory;
using QuickHire.Application.Users.Buyer.FirstPage.RecentlyLiked;
using QuickHire.Application.Users.Buyer.FirstPage.RecentlyViewed;
using QuickHire.Application.Users.Buyer.FirstPage.SeeMore;
using QuickHire.Application.Users.Models.Profile;
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
        })
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
        .WithTags("Buyer Project Briefs")
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

        app.MapPut("/buyers/profile/edit", async ([FromBody] UpdateProfileCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("EditBuyerProfile")
        .WithTags("Profile")
        .WithDescription("Edit the buyer's profile information.");

        #endregion   

        #region First Page
        app.MapGet("/buyers/recently-liked", async([AsParameters] RecentlyLikedQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetRecentlyLikedGigs")
        .WithTags("First Page")
        .WithDescription("Gets the recently liked gigs for the buyer.");

        app.MapGet("/buyers/recently-viewed", async([AsParameters] RecentlyViewedQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetRecentlyViewedGigs")
        .WithTags("First Page")
        .WithDescription("Gets the recently viewed gigs for the buyer.");

        app.MapGet("/buyers/see-more", async([AsParameters] SeeMoreQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("SeeMoreGigs")
        .WithTags("First Page")
        .WithDescription("Gets more gigs for the buyer based on the provided criteria.");

        app.MapGet("/buyers/hot-in-main-category", async([AsParameters] HotGigsOnMainCategoryQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetHotGigsInMainCategory")
        .WithTags("First Page")
        .WithDescription("Gets hot gigs in a main category by Id.");

        app.MapGet("/buyers/explore", async([AsParameters] ExploreHotGigsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("ExploreGigs")
        .WithTags("First Page")
        .WithDescription("Gets gigs for the buyer to explore.");
        #endregion

        #region CategoryPages
        app.MapGet("sub-categories/page/{Id}", async ([AsParameters] SubCategoryPageDataQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetSubCategoryPageDetails")
        .WithTags("Sub Categories")
        .WithDescription("Gets sub category page details by Id.");

        app.MapGet("sub-sub-categories/page/{Id}", async ([AsParameters] SubSubCategoryPageDataQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetSubSubCategoryPageDetails")
        .WithTags("Sub Sub Categories")
        .WithDescription("Gets sub sub category page details by Id.");

        app.MapGet("sub-sub-categories-in-sub-category/{Id}", async ([AsParameters] SubSubCategoriesInSubCategoryQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetSubSubCategoriesInSubCategory")
        .WithTags("Sub Sub Categories")
        .WithDescription("Gets sub sub categories in a sub category by Id.");
        #endregion

        #region Report
        app.MapPut("/admin/report", async([FromBody] ReportItemCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("ReportItem")
        .WithTags("Report")
        .WithDescription("Report a user for inappropriate content or behavior.");
        #endregion

        #region CustomOffers 
        app.MapGet("buyer/custom-offers/{Id}", async ([AsParameters] GetCustomOfferQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "buyer" })
        .WithName("GetCustomOffer")
        .WithTags("Custom Offers")
        .WithDescription("Get a custom offer by its ID.");
        #endregion
    }
}

