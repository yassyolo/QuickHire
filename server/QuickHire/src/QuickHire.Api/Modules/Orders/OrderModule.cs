using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Orders.Models.Reviews;
using QuickHire.Application.Orders.Ratings.Reviews;
using QuickHire.Application.Orders.Reviews.GetRatings;
using QuickHire.Application.Orders.Reviews.RatingDistribution;
using QuickHire.Application.Users.Buyer.Profile;

namespace QuickHire.Api.Modules.Orders;

public class OrderModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region Reviews
        app.MapGet("/orders/reviews", async ([AsParameters] GetReviewsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetRatings")
   .WithTags("Orders")
   .Produces<List<ReviewResponseRowModel>>(StatusCodes.Status200OK)
   .WithDescription("Get ratings");

        app.MapGet("/orders/reviews/ratings-distribution", async ([AsParameters] GetRatingDistributionQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetRatingDistribution")
            .WithTags("Orders")
            .WithDescription("Get rating distribution");

        app.MapPost("/orders/reviews", async ([FromBody] AddReviewCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("AddRating")
            .WithTags("Orders")
            .WithDescription("Add a rating");
        #endregion

    }
}
