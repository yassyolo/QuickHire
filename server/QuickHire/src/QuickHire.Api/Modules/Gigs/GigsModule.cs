using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.Gigs.DeactivateGigAdmin;
using QuickHire.Application.Admin.Models.Gigs;
using QuickHire.Application.Gigs.Details.GigDetails;
using QuickHire.Application.Gigs.Details.SellerDetails;
using QuickHire.Application.Gigs.FavouriteLists.AddFavouriteList;
using QuickHire.Application.Gigs.FavouriteLists.DeleteGigList;
using QuickHire.Application.Gigs.FavouriteLists.EditFavouriteList;
using QuickHire.Application.Gigs.FavouriteLists.GetFavouriteListItems;
using QuickHire.Application.Gigs.FavouriteLists.GetFavouriteLists;
using QuickHire.Application.Gigs.FavouriteLists.PopulateFavouriteGigsList;
using QuickHire.Application.Gigs.FavouriteLists.RemoveGigFromList;
using QuickHire.Application.Gigs.FavouriteLists.SaveGigToOldList;
using QuickHire.Application.Gigs.FavouriteLists.UnfavouriteGig;
using QuickHire.Application.Gigs.GigsInSubSubCategory;
using QuickHire.Application.Gigs.Models.FavouriteLists;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Application.Gigs.Models.Tags;
using QuickHire.Application.Gigs.Seller.SellerDetails;
using QuickHire.Application.Gigs.Statistics.CustomerFeedback.RatingDistribution;
using QuickHire.Application.Gigs.Statistics.CustomerFeedback.ReviewResponseRate;
using QuickHire.Application.Gigs.Statistics.CustomerFeedback.Reviews;
using QuickHire.Application.Gigs.Statistics.CustomerFeedback.Stars;
using QuickHire.Application.Gigs.Statistics.Engagement.Likes;
using QuickHire.Application.Gigs.Statistics.Engagement.Views;
using QuickHire.Application.Gigs.Statistics.OrderFulfillment.AverageDeliveryTime;
using QuickHire.Application.Gigs.Statistics.OrderFulfillment.Orders;
using QuickHire.Application.Gigs.Statistics.OrderFulfillment.OrderStatus;
using QuickHire.Application.Gigs.Statistics.OrderFulfillment.Revisions;
using QuickHire.Application.Gigs.Statistics.SalesAndRevenue.AverageOrderValue;
using QuickHire.Application.Gigs.Statistics.SalesAndRevenue.PaymentPlanChoice;
using QuickHire.Application.Gigs.Statistics.SalesAndRevenue.Revenue;
using QuickHire.Application.Gigs.Statistics.SalesAndRevenue.SalesVolume;
using QuickHire.Application.Gigs.Tags.GetTags;
using QuickHire.Application.Users.Buyer.BrowsingHistory.BrowsingHistory;
using QuickHire.Application.Users.Buyer.BrowsingHistory.BrowsingHistoryRow;
using QuickHire.Application.Users.Buyer.BrowsingHistory.DeleteBrowsingHistory;
using QuickHire.Domain.Users;

namespace QuickHire.Api.Modules.Gigs;

public class GigsModule : CarterModule
{
    //GET /gigs/statistics/rating-distribution-statistics?id=123
    //GET /gigs/statistics/review-response-statistics?id=123


    public override void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/gigs/statistics/views-statistics/{id}", async ([AsParameters] ViewStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetViewsStatistics")
        .WithTags("Gigs")
        .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Get views statistics for a gig");

        app.MapGet("/gigs/statistics/likes-statistics/{id}", async([AsParameters] LikesStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetLikesStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get likes statistics for a gig");

        app.MapGet("/gigs/statistics/reviews-statistics/{id}", async([AsParameters] ReviewsStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetReviewsStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get reviews statistics for a gig");

        app.MapGet("/gigs/statistics/stars-statistics/{id}", async ([AsParameters] StarsStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetStarsStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get stars statistics for a gig");
        app.MapGet("/gigs/statistics/sales-volume-statistics/{id}", async([AsParameters] SalesVolumeStatisticsVolumeQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetSalesVolumeStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get sales volume statistics for a gig");

        app.MapGet("/gigs/statistics/payment-plan-choice-statistics/{id}", async ([AsParameters] PaaymentPlanChoiceStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetPaymentPlanChoiceStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get payment plan choice statistics for a gig");
            

        app.MapGet("/gigs/statistics/average-order-value-statistics/{id}", async([AsParameters] AverageOrderValueStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetAverageOrderValueStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get average order value statistics for a gig");

        app.MapGet("/gigs/statistics/revenue-statistics/{id}", async ([AsParameters] RevenueStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetRevenueStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get revenue statistics for a gig");

        app.MapGet("/gigs/statistics/review-response-statistics/{id}", async ([AsParameters] ReviewResponseRateStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetReviewResponseStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get review response statistics for a gig");

        app.MapGet("/gigs/statistics/rating-distribution-statistics/{id}", async ([AsParameters] RatingDistributionStatisticsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetRatingDistributionStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get rating distribution statistics for a gig");

        app.MapGet("/gigs/statistics/average-delivery-time-statistics/{id}", async ([AsParameters] AverageDeliveryTimeQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetAverageDeliveryTimeStatistics")
            .WithTags("Gigs")
                .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .WithDescription("Get average delivery time statistics for a gig");
            app.MapGet("/gigs/statistics/order-statistics/{id}", async ([AsParameters] OrdersStatisticsQuery query, IMediator mediator) =>
           {
               var result = await mediator.Send(query);
               return Results.Ok(result);
           })
            .WithName("GetOrdersStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get orders statistics for a gig");
            
            app.MapGet("/gigs/statistics/order-status-statistics/{id}", async ([AsParameters] OrderStatusStatisticsQuery query, IMediator mediator) =>
            {
                var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .WithName("GetOrderStatusStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .WithDescription("Get order status statistics for a gig");
             app.MapGet("/gigs/statistics/revision-statistics/{id}", async ([AsParameters] RevisionsStatisticsQuery query, IMediator mediator) =>
            {
                var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .WithName("GetRevisionsStatistics")
            .WithTags("Gigs")
            .Produces<StatisticsLineChartModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .WithDescription("Get revisions statistics for a gig");


        #region Tags
        app.MapGet("/tags", async ([AsParameters] GetTagsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetTags")
        .WithTags("Gigs")
        .Produces<GetTagsResponseModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Get tags for a gig or main category");
        #endregion

        #region BrowsingHistory
            app.MapGet("/buyers/browsing-history/row", async([AsParameters] GetBrowsingHistoryRowQuery query, IMediator mediator) =>
            {
                   var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .WithName("GetBrowsingHistoryRow")
            .WithTags("Buyers")
            .Produces<IEnumerable<BrowsingHistoryRowModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get browsing history for a buyer");

                app.MapDelete("/buyers/browsing-history", async([AsParameters] DeleteBrowsingHistoryQuery query, IMediator mediator) =>
                {
           await mediator.Send(query);
                    return Results.NoContent();
                })
        .WithName("DeleteBrowsingHistory")
            .WithTags("Buyers")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Delete browsing history for a buyer");

        app.MapGet("/buyers/browsing-history", async([AsParameters] BrowsingHistoryQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetBrowsingHistory")
            .WithTags("Buyers")
            .Produces<IEnumerable<BrowsingHistoryRowModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get browsing history for a buyer");


        #endregion

        #region FavouriteList
        //buyer/favourite-gigs/lists/populate
        app.MapGet("/buyers/favourite-gigs/lists/populate", async([AsParameters] PopulateFavouriteGigsListQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("PopulateFavouriteGigsList")
            .WithTags("FavouriteGigs")
            .Produces<IEnumerable<PopulateFavouriteGigListModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get favourite gigs list populated for a buyer");

        //buyer/favourite-gigs/add/{id}
        app.MapPost("/buyers/favourite-gigs/add", async([FromBody] SaveGigToOldListCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("AddGigToOldList")
            .WithTags("FavouriteGigs")

            .WithDescription("Add a gig to an old favourite list for a buyer");
        //buyer/favourite-gigs/delete/{id}
        app.MapDelete("/buyers/favourite-gigs/delete/{id}", async([FromBody] RemoveGigFromListCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("DeleteGigFromOldList")
            .WithTags("FavouriteGigs")
            .WithDescription("Delete a gig from an old favourite list for a buyer");

        //buyer/favourite-gigs
        app.MapPost("/buyers/favourite-gigs", async([FromBody] AddFavouriteListCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("AddNewFavouriteGigsList")
            .WithTags("FavouriteGigs")
            .WithDescription("Add a new favourite gigs list for a buyer");


        app.MapDelete("/buyers/favourite-list/{id}", async([AsParameters] DeleteGigListCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("RemoveFavouriteGigList")
            .WithTags("FavouriteGigs")
            .WithDescription("Remove a gig list for a buyer");

        app.MapPut("/buyers/favourite-gigs", async([FromBody] EditFavouriteListCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("EditFavouriteGigList")
            .WithTags("FavouriteGigs")
            .WithDescription("Edits a gig list for a buyer");

        app.MapGet("buyers/favourite-gigs/lists", async([AsParameters] GetFavouriteListsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetFavouriteGigsLists")
            .WithTags("FavouriteGigs")
            .WithDescription("Get favourite gigs lists for a buyer");

        app.MapGet("buyers/favourite-gigs/lists/{id}", async([AsParameters] GetFavouriteListItemsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })  
            .WithName("GetFavouriteGigsListById")
            .WithTags("FavouriteGigs")

            .WithDescription("Get a favourite gigs list by id for a buyer");

        app.MapPut("buyers/favourite-gigs/unfavourite/{id}", async([AsParameters] UnfavouriteGigCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("RemoveGigFromFavourite")
            .WithTags("FavouriteGigs")
            .WithDescription("Remove a gig from favourite for a buyer");


        #endregion
        app.MapGet("/gigs/seller", async([AsParameters] GetSellerDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetGigSeller")
            .WithTags("Gigs")
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Get gigs for a seller");

        app.MapGet("/gigs/seller-details/{Id}", async([AsParameters] SellerDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetGigSellerDetails")
            .WithTags("Gigs")
            .WithDescription("Get seller details by gig id");

       app.MapGet("/gigs", async([AsParameters] GetGigDetailsQuery query, IMediator mediator) =>
       {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GigDetails")
        .WithTags("Gigs")
        .WithDescription("Search gigs with pagination and filtering options");

        app.MapPost("/sub-sub-category/gigs", async ([FromBody] GigsInSubSubCategoryQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
         .WithName("GetGigsInSubSubCategory")
         .WithTags("Gigs")
         .WithDescription("Get gigs in a sub-sub-category with pagination and filtering options");

    }
}
