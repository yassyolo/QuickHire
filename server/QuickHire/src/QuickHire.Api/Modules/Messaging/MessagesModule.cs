using Carter;
using MediatR;
using QuickHire.Application.Users.Messaging.GetAllMessages;
using QuickHire.Application.Users.Messaging.GetCurrentConversation;
using QuickHire.Application.Users.Messaging.ToggleConversationLike;

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

        //GetCurrentConversationQuery
        app.MapGet("/messages/{Id}", async ([AsParameters] GetCurrentConversationQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
            .WithName("GetCurrentConversation")
            .WithTags("Messaging")
            .WithDescription("Get the current conversation by Id");

        //ToggleConversationStarCommand
        app.MapPost("/messages/star/{Id}", async([AsParameters] ToggleConversationStarCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("ToggleConversationStar")
            .WithTags("Messaging")
            .WithDescription("Toggle the star status of a conversation message");

    }
}
