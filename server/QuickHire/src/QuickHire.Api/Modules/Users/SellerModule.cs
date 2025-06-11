using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Application.Users.Models.Dashboard;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Application.Users.Models.Statistics;
using QuickHire.Application.Users.ProjectBriefs.ProjectBriefPreview;
using QuickHire.Application.Users.Seller.CustomOffers.ChooseFromGigs;
using QuickHire.Application.Users.Seller.CustomOffers.ChooseFromInclusives;
using QuickHire.Application.Users.Seller.Dashboard.GetSellerDashboard;
using QuickHire.Application.Users.Seller.Dashboard.GetSellerDashboardOrders;
using QuickHire.Application.Users.Seller.Gigs.DeleteGig;
using QuickHire.Application.Users.Seller.Gigs.GetGigForDelete;
using QuickHire.Application.Users.Seller.Gigs.SellerGigs;
using QuickHire.Application.Users.Seller.Gigs.SellerGigsTable;
using QuickHire.Application.Users.Seller.Gigs.ToggleActivationStatus;
using QuickHire.Application.Users.Seller.NewSeller.GetExistingUserInfo;
using QuickHire.Application.Users.Seller.Orders.GetOrdersTable;
using QuickHire.Application.Users.Seller.Profile.EditCertification;
using QuickHire.Application.Users.Seller.Profile.EditDescription;
using QuickHire.Application.Users.Seller.Profile.EditEducation;
using QuickHire.Application.Users.Seller.Profile.EditPortfolio;
using QuickHire.Application.Users.Seller.Profile.EditSkill;
using QuickHire.Application.Users.Seller.Profile.GetSellerProfile;
using QuickHire.Application.Users.Seller.Profile.PopulateLAnguages;
using QuickHire.Application.Users.Seller.ProjectBriefs.GetProjectBriefsTable;
using QuickHire.Application.Users.Seller.Statistics.Earnings.Cards;
using QuickHire.Application.Users.Seller.Statistics.Earnings.Statistics;
using QuickHire.Application.Users.Seller.Statistics.Engagement.Cards;
using QuickHire.Application.Users.Seller.Statistics.Engagement.Statistics;
using QuickHire.Application.Users.Seller.Statistics.GigPerformance.Cards;
using QuickHire.Application.Users.Seller.Statistics.GigPerformance.Statistics;
using QuickHire.Application.Users.Seller.Statistics.OrderFullfillment.Cards;
using QuickHire.Application.Users.Seller.Statistics.OrderFullfillment.Statistics;
using QuickHire.Application.Users.Seller.Statistics.RepeatBusiness.Cards;
using QuickHire.Application.Users.Seller.Statistics.RepeatBusiness.Statistics;

namespace QuickHire.Api.Modules.Users;

public class SellerModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
            #region Statistics  

        app.MapGet("/seller/statistics/gig-performance", async([AsParameters] GetGigPerformanceStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetGigPerformanceStatistics")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<IEnumerable<GigPerformanceRowModel>>(StatusCodes.Status200OK);

        app.MapGet("/seller/statistics/engagement", async([AsParameters] GetEngagementStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetEngagementStatistics")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<IEnumerable<EngagementStatisticsRowModel>>(StatusCodes.Status200OK);

        app.MapGet("/seller/statistics/earnings", async([AsParameters] GetEarningStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetEarningStatistics")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<IEnumerable<EarningStatisticsRowModel>>(StatusCodes.Status200OK);

        app.MapGet("/seller/statistics/order-fulfillment", async([AsParameters] GetOrderFullfillmentStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetOrderFullfillmentStatistics")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<IEnumerable<OrderFullfillmentRowModel>>(StatusCodes.Status200OK);

        app.MapGet("/seller/statistics/repeat-business", async([AsParameters] GetRepeatBusinessStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetRepeatBusinessStatistics")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<IEnumerable<RepeatBusinessRowModel>>(StatusCodes.Status200OK);


        app.MapGet("/seller/statistics/gig-performance/cards", async ([AsParameters] GetGigPerformanceCardsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetGigPerformanceCards")
        .WithTags("Seller")
        .Produces(StatusCodes.Status404NotFound)
        .Produces<IEnumerable<CardItemModel>>(StatusCodes.Status200OK);

        app.MapGet("/seller/statistics/earnings/cards", async ([AsParameters] GetEarningStatisticsCardsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetEarningStatisticsCards")
    .WithTags("Seller")
    .Produces(StatusCodes.Status404NotFound)
    .Produces<IEnumerable<CardItemModel>>(StatusCodes.Status200OK);

        app.MapGet("/seller/statistics/order-fulfillment/cards", async ([AsParameters] GetOrderFullfillmentStatisticsCradsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetOrderFullfillmentStatisticsCards")
        .WithTags("Seller")
        .Produces(StatusCodes.Status404NotFound)
        .Produces<IEnumerable<CardItemModel>>(StatusCodes.Status200OK);

        app.MapGet("/seller/statistics/engagement/cards", async ([AsParameters] GetEngagementStatisticsCardsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetEngagementStatisticsCards")
            .WithTags("Seller")
        .Produces(StatusCodes.Status404NotFound)
        .Produces<IEnumerable<CardItemModel>>(StatusCodes.Status200OK);

        app.MapGet("/seller/statistics/repeat-business/cards", async ([AsParameters] GetRepeatBusinessStatisticsCardsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetRepeatBusinessStatisticsCards")
        .WithTags("Seller")
        .Produces(StatusCodes.Status404NotFound)
        .Produces<IEnumerable<CardItemModel>>(StatusCodes.Status200OK);

        #endregion

        #region Dashboard
        app.MapGet("/seller/dashboard", async([AsParameters] GetSellerDashboardQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSellerDashboard")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<GetSellerDashboardModel>(StatusCodes.Status200OK)
            .WithDescription("Get the seller dashboard data including statistics and other relevant information.");

        app.MapGet("/seller/dashboard/orders", async([AsParameters] GetSellerDashboardOrdersQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
         .WithName("GetSellerDashboardOrders")
         .WithTags("Seller")
         .Produces(StatusCodes.Status404NotFound)
         .Produces<IEnumerable<SellerDashboardOrderModel>>(StatusCodes.Status200OK)
         .WithDescription("Get the seller dashboard orders data.");
        #endregion

        #region Profile
        app.MapGet("/seller/profile", async ([AsParameters] GetSellerProfileQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSellerProfile")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<SellerProfileModel>(StatusCodes.Status200OK)
            .WithDescription("Get the seller profile data including personal information, statistics, and other relevant details.");

        app.MapPut("/seller/profile/description", async([FromBody] EditDescriptionCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("EditSellerDescription")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<SellerProfileModel>(StatusCodes.Status200OK)
            .WithDescription("Edit the seller's profile description.");

        app.MapPut("/seller/profile/certifications", async([FromBody] EditCertificationCommand command, IMediator mediator) =>
        {
            Console.WriteLine(command);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("EditSellerCertifications")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Edit the seller's certifications in their profile.");

        app.MapPut("/seller/profile/educations", async ([FromBody] EditEducationCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("EditSellerEducations")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Edit the seller's educations in their profile.");

        app.MapPut("/seller/profile/skills", async ([FromBody] EditSkillCommand command, IMediator mediator) =>
        {
             var result = await mediator.Send(command);
             return Results.Ok(result);
         })
        .WithName("EditSellerSkills")
        .WithTags("Seller")
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
         .WithDescription("Edit the seller's skills in their profile.");

        app.MapPut("/seller/profile/portfolio", async ([FromForm] EditPortfolioCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("EditSellerPortfolio")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<SellerProfileModel>(StatusCodes.Status200OK)
            .DisableAntiforgery()
            .WithDescription("Edit the seller's portfolio in their profile.");

        app.MapGet("/languages/populate", async ([AsParameters] PopulateLanguagesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("PopulateLanguages")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<IEnumerable<PopulationModel>>(StatusCodes.Status200OK)
            .WithDescription("Populate the languages for the seller's profile.");



    #endregion

       // /seller/Gigs/delete
       app.MapDelete("/seller/gigs/delete/{Id}", async([AsParameters] DeleteGigCommand command, IMediator mediator) =>
       {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("DeleteGig")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Delete a gig for the seller.");

        app.MapGet("/seller/gigs/delete/{Id}", async ([AsParameters] GetGigForDeleteQuery command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("GetGigForDelete")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get gig details for deletion confirmation.");

        app.MapPut("/seller/gigs/activate", async([AsParameters] ToggleActivationStatusCommand command, IMediator mediator) =>
        {
             await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("ActivateGig")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Activate a gig for the seller.");
        app.MapGet("/seller/gigs/table", async([AsParameters] GetSellerGigsTableQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetSellerGigsTable")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<IEnumerable<GigCardModel>>(StatusCodes.Status200OK)
         .WithDescription("Get the seller's gigs table.");

        app.MapGet("/seller/gigs", async ([AsParameters] GetSellerGigsQuery query, IMediator mediator) =>
{
            var result = await mediator.Send(query);        
          return Results.Ok(result);
         })
            .WithName("GetSellerGigs")
        .WithTags("Seller")
        .Produces(StatusCodes.Status404NotFound)
        .Produces<IEnumerable<GigCardModel>>(StatusCodes.Status200OK)
        .WithDescription("Get the seller's gigs with pagination and filtering options.");

        //                const url = `https://localhost:7267/seller/orders/table?${params.toString()}`;


        app.MapGet("/seller/orders/table", async([AsParameters] GetOrdersTableQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSellerOrdersTable")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get the seller's orders table with pagination and filtering options.");

        //https://localhost:7267/seller/project-briefs/table
        app.MapGet("/seller/project-briefs/table", async([AsParameters] GetProjectBriefsTableQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSellerProjectBriefsTable")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get the seller's project briefs table with pagination and filtering options.");

        //      const res = await axios.get("https://localhost:7267/seller/new");
        app.MapGet("/seller/new", async ([AsParameters]  GetExistingUserInfoQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
                .WithName("NewSellerInfo")
                .WithTags("Seller")
                .Produces(StatusCodes.Status200OK)
                .WithDescription("Get info for new seller form.");

        app.MapGet("/project-brief/preview/{Id}", async([AsParameters] ProjectBriefPreviewQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetProjectBriefPreview")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get the preview of a project brief by its ID.");

        #region Custom Offers
         app.MapGet("/seller/choose-from-gigs", async([AsParameters] ChooseFromGigsQuery query, IMediator mediator) =>
         {
            var result = await mediator.Send(query);
            return Results.Ok(result);
})
            .WithName("ChooseFromGigs")
            .WithTags("Custom Offers")
            .WithDescription("Get a list of gigs to choose from for custom offers.");

        app.MapGet("/seller/choose-from-inclusives", async ([AsParameters] ChooseFromInclusivesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
   .WithName("ChooseFromInclusives")
   .WithTags("Custom Offers")
   .WithDescription("Get a list of gigs to choose from for custom offers.");
        #endregion
    }
}
