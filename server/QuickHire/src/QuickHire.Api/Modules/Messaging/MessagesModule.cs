using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Users.Messaging.GetAllMessages;
using QuickHire.Application.Users.Messaging.GetConversationForOrder;
using QuickHire.Application.Users.Messaging.GetCurrentConversation;
using QuickHire.Application.Users.Messaging.ToggleConversationLike;
using QuickHire.Application.Users.Messaging.UploadFile;

namespace QuickHire.Api.Modules.Messaging;

public class MessagesModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        //GetAllMessagesQuery
        app.MapGet("/messages", async ([AsParameters] GetAllMessagesQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetAllMessages")
        .WithTags("Messaging")
        .WithDescription("Get all messages in the current conversation");

        app.MapGet("/messages/{Id}", async ([AsParameters] GetCurrentConversationQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetCurrentConversation")
            .WithTags("Messaging")
            .WithDescription("Get the current conversation by Id");

        app.MapGet("/messages/order/{Id}", async ([AsParameters] GetConversationForOrderQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetConversationForOrder")         
           .WithTags("Messaging")
           .WithDescription("Get the current conversation by Id");

        app.MapPost("/messages/star", async([AsParameters] ToggleConversationStarCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("ToggleConversationStar")
            .WithTags("Messaging")
            .WithDescription("Toggle the star status of a conversation message");

        app.MapPost("/files/upload", async ([FromForm] UploadFileCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
.WithName("UploadFile")
.DisableAntiforgery()
.WithTags("Files")
.WithDescription("Uploads a file and returns its URL.");

    }
}
