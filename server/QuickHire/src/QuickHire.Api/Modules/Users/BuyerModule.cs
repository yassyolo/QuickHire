using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Users.Authentication.ChangePassword;
using QuickHire.Application.Users.Buyer.AddBuyerDetails;
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

    app.MapPost("/buyers/project-briefs", async ([FromBody] AddProjectBriefCommand command, IMediator mediator) =>
    {
        await mediator.Send(command);
        return Results.NoContent();
    })
    .WithName("AddProjectBrief")
    .WithTags("Buyer")
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .WithDescription("Add a new project brief for the buyer.");

        //GetBuyerProjectBriefsQuery
        app.MapGet("/buyers/project-briefs", async ([AsParameters] GetBuyerProjectBriefsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }
        )
            .WithName("GetBuyerProjectBriefs")
            .WithTags("Buyer")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Get all project briefs for the buyer.");

        app.MapGet("/buyers/profile", async ([AsParameters] GetBuyerProfileQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetBuyerProfile")
            .WithTags("Buyer")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Get the buyer's profile information.");

        app.MapPost("/buyers/profile", async ([AsParameters] AddBuyerDetailsCommand query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("AddBuyerDetails")
            .WithTags("Buyer")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Add or update the buyer's profile details.");

        app.MapPut("user/languages", async ([FromBody] EditLanguagesCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("EditUserLanguages")
            .WithTags("Seller")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<SellerProfileModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Edit the languages of the user.");   

        //      const url = `https://localhost:7267/buyer/project-briefs/delete/${id}`;
        app.MapDelete("/buyers/project-briefs/delete/{id}", async ([AsParameters] WithdrawProjectBriefCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
            .WithName("DeleteProjectBrief")
            .WithTags("Buyer")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Delete a project brief by ID.");



    }
}

