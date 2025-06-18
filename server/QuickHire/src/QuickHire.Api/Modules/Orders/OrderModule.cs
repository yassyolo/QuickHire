using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Orders.Models.Reviews;
using QuickHire.Application.Orders.OrderDetails;
using QuickHire.Application.Orders.PlaceOrder.OrderForm;
using QuickHire.Application.Orders.PlaceOrder.SubmitOrder;
using QuickHire.Application.Orders.PlaceOrder.SuccessfulPayment;
using QuickHire.Application.Orders.Ratings.Reviews;
using QuickHire.Application.Orders.Reviews.GetRatings;
using QuickHire.Application.Orders.Reviews.RatingDistribution;
using Stripe;

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

        ///orders
        app.MapPost("/orders", async([FromBody] SubmitOrderCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("CreateOrder")
            .WithTags("Orders")
            .WithDescription("Create a new order");
        ///orders/form
        app.MapGet("/orders/form", async([AsParameters] GetOrderFormQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetOrdersForm")
            .WithTags("Orders")
            .WithDescription("Get buyer profile for order form");

        //"mark-paid
        app.MapPost("/mark-paid", async([FromBody] MarkOrderPaidCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("MarkOrderAsPaid")
            .WithTags("Orders")
            .WithDescription("Mark order as paid");

        //orders/
        app.MapGet("/orders/{Id}", async([AsParameters] GetOrderDetailsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetOrderById")
            .WithTags("Orders")
            .WithDescription("Get order by ID");

    }
}
