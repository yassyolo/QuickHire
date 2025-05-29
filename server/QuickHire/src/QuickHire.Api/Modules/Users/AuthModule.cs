using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Users.Authentication.AboutUser;
using QuickHire.Application.Users.Authentication.GoogleLogin;
using QuickHire.Application.Users.Authentication.Login;
using QuickHire.Application.Users.Authentication.Logout;
using QuickHire.Application.Users.Authentication.RefreshToken;
using QuickHire.Application.Users.Authentication.Register;
using QuickHire.Application.Users.Authentication.VerifyEmail;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Infrastructure.Persistence.Identity;

namespace QuickHire.Api.Modules.Users;

public class AuthModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", async ([FromBody] LoginBuyerModel model, IMediator mediator) =>
        {
            await mediator.Send(new LoginBuyerCommand(model));

            return Results.NoContent();
        })
        .WithName("LoginBuyer")
        .Produces<Unit>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .ProducesValidationProblem()
        .WithDescription("Authenticates a buyer using email or username and password.");

        app.MapGet("/auth/google", (HttpContext context, LinkGenerator linkGenerator, SignInManager<ApplicationUser> signInManager) =>
        {
            var returnUrl = context.Request.Query["returnUrl"].ToString();

            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", "/signin-google");
            properties.Items["returnUrl"] = returnUrl;
            return Results.Challenge(properties, new[] { "Google" });
        })
            .WithName("GoogleLogin")
            .Produces(StatusCodes.Status302Found)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Redirects to Google for authentication. After successful authentication, redirects back to the specified return URL.");

        app.MapGet("/signin-google", async (HttpContext context, IMediator mediator) =>
        {
            var returnUrl = context.Request.Query["returnUrl"].ToString() ?? "/";
            await mediator.Send(new GoogleLoginCommand(context, returnUrl));

            return Results.Ok();
        })
            .WithName("GoogleLoginCallback")
            .Produces(StatusCodes.Status302Found)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithDescription("Handles the callback from Google after authentication. This endpoint is called by Google after the user has authenticated. It processes the authentication result and redirects the user accordingly.");

        app.MapPost("auth/register", async ([FromBody] RegisterBuyerCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("RegisterBuyer")
        .Produces<RegisterUserResponseModel>(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status400BadRequest)
        .WithDescription("Registers a new buyer. If successful, returns a 201 Created status with the user's details.");

        app.MapGet("auth/me", async (IMediator mediator, HttpContext context) =>
        {
            if (context.User.Identity?.IsAuthenticated != true)
            {
                return Results.Unauthorized();
            }

            var result = await mediator.Send(new AboutUserQuery());

            return Results.Ok(result);
        })
        .WithName("AboutMe")
        .Produces<AboutUserModel>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .WithDescription("Retrieves information about the currently authenticated user. If the user is not authenticated, returns a 401 Unauthorized status. If the user is not found, returns a 404 Not Found status.");

        app.MapPost("auth/refresh-token", async ([FromBody] RefreshTokenCommand command, IMediator mediator) =>
        {

            var result = await mediator.Send(command);

            return Results.Ok(result);
        })
         .WithName("RefreshToken")
         .Produces(StatusCodes.Status204NoContent)
         .Produces(StatusCodes.Status401Unauthorized)
         .Produces(StatusCodes.Status404NotFound)
         .WithDescription("Refreshes the JWT token using a valid refresh token stored in a cookie or sent in the request body.");

        app.MapGet("/auth/verify-email", async ([AsParameters] VerifyEmailCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Redirect("http://localhost:5173/admin/main-categories");
        })
        .WithName("VerifyEmail")
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .WithDescription("Verifies a user's email using userId and token from the query string.");

        app.MapPost("/auth/logout", async ([FromBody] LogoutCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);

            return Results.NoContent();
        })
        .WithName("Logout")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .WithDescription("Logs out the user by clearing the authentication cookies.");
    }
}
