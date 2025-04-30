using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickHire.Application.Users.Authentication.Login;
using QuickHire.Application.Users.Authentication.Register;
using QuickHire.Infrastructure.Persistence.Identity;

namespace QuickHire.Api.Modules;

public class AuthModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", async (LoginBuyerCommand command, IValidator<LoginBuyerCommand> validator, IMediator mediator) =>
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("LoginBuyer")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status401Unauthorized)
        .WithDescription("Authenticates a buyer using email or username and password.");

        app.MapGet("/auth/google", (HttpContext context, LinkGenerator linkGenerator, SignInManager<ApplicationUser> signInManager) =>
        {
            var returnUrl = context.Request.Query["returnUrl"].ToString();

            var generatedLink = linkGenerator.GetPathByName(context, "GoogleLoginCallback");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", generatedLink + $"?returnUrl={returnUrl}");

            return Results.Challenge(properties, ["Google"]);
        })
            .WithName("GoogleLogin")
            .Produces(StatusCodes.Status302Found)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Redirects to Google for authentication. After successful authentication, redirects back to the specified return URL.");

        app.MapGet("/auth/google/callback", async (HttpContext context, SignInManager<ApplicationUser> signInManager) => { })
            .WithName("GoogleLoginCallback")
            .Produces(StatusCodes.Status302Found)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithDescription("Handles the callback from Google after authentication. This endpoint is called by Google after the user has authenticated. It processes the authentication result and redirects the user accordingly.");

        /*app.MapPost("auth/register", async (RegisterBuyerCommand command, IValidator<RegisterBuyerCommand> validator, IMediator mediator) =>
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await mediator.Send(command);
            if (result.)
            {
                return Results.Created($"/auth/register/{result.UserId}", result);
            }
            return Results.Conflict(result);

        })
        .WithName("RegisterBuyer")
        .Produces(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status409Conflict)
        .WithDescription("Registers a new buyer using email and password. If the email is already in use, it returns a conflict status. If the registration is successful, it returns a 201 Created status.");*/
    }
}
