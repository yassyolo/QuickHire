using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Admin.FAQ.AddFAQ;
using QuickHire.Application.Admin.FAQ.DeleteFAQ;
using QuickHire.Application.Admin.FAQ.EditFAQ;
using QuickHire.Application.Admin.FAQ.GetFAQ;
using QuickHire.Application.Admin.Models.FAQ;

namespace QuickHire.Api.Modules.Shared;

public class FAQModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region FAQs 
        
        //faqs/add
        app.MapPost("/faqs", async([FromBody] AddFAQCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
            .WithName("AddFAQ")
            .WithTags("FAQs")
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithDescription("Adds a new FAQ for a gig or main category.");
        app.MapPut("/faqs", async ([FromForm] EditFAQCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("EditFAQ")
                    .WithTags("FAQs")
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .DisableAntiforgery()
        .WithDescription("Edits an existing FAQ by Id.");

        app.MapGet("/faqs", async ([AsParameters] GetFAQQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("SearchFAQs")
                    .WithTags("FAQs")
        .Produces<List<FAQResponseModel>>(StatusCodes.Status200OK)
        .WithDescription("Searches through FAQs by gig, main category or user id.");

        app.MapDelete("/faqs", async ([FromBody] DeleteFAQCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("DeleteFAQ")
                    .WithTags("FAQs")
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem()
        .WithDescription("Deletes a FAQ by Id.");
        #endregion
    }
}
