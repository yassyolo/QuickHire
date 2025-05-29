using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.Gigs.DeactivateGigAdmin;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Application.Gigs.Models.Tags;
using QuickHire.Application.Gigs.Statistics.CustomerFeedback.RatingDistribution;
using QuickHire.Application.Gigs.Statistics.CustomerFeedback.ReviewResponseRate;
using QuickHire.Application.Gigs.Statistics.CustomerFeedback.Reviews;
using QuickHire.Application.Gigs.Statistics.CustomerFeedback.Stars;
using QuickHire.Application.Gigs.Statistics.Engagement.Likes;
using QuickHire.Application.Gigs.Statistics.Engagement.Views;
using QuickHire.Application.Gigs.Statistics.SalesAndRevenue.AverageOrderValue;
using QuickHire.Application.Gigs.Statistics.SalesAndRevenue.Revenue;
using QuickHire.Application.Gigs.Statistics.SalesAndRevenue.SalesVolume;
using QuickHire.Application.Gigs.Tags.GetTags;

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

        #region Admin
        app.MapPut("/admin/gig/deactivate", async([FromBody] DeactivateGigAdminCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("DeactivateGig")
        .WithTags("Admin")
        .Produces<Unit>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Deactivate a gig");
        
        #endregion
    }
}
