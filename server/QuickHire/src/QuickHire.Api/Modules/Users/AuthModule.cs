using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Users.Authentication.AboutUser;
using QuickHire.Application.Users.Authentication.ChangePassword;
using QuickHire.Application.Users.Authentication.GoogleLogin;
using QuickHire.Application.Users.Authentication.Login;
using QuickHire.Application.Users.Authentication.Logout;
using QuickHire.Application.Users.Authentication.RefreshToken;
using QuickHire.Application.Users.Authentication.Register;
using QuickHire.Application.Users.Authentication.SwitchMode;
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
        .WithTags("Authentication")
        .WithDescription("Authenticates a buyer using email or username and password.");

        app.MapGet("/auth/google", (HttpContext context) =>
        {
            var returnUrl = context.Request.Query["returnUrl"].ToString() ?? "/";
            var properties = new AuthenticationProperties
            {
                RedirectUri = $"/signin-google?returnUrl={Uri.EscapeDataString(returnUrl)}"
            };
            return Results.Challenge(properties, new[] { GoogleDefaults.AuthenticationScheme });
        });

        app.MapGet("/auth/google-callback", async (HttpContext context, IMediator mediator) =>
        {
            var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return Results.BadRequest("Authentication failed");

            var returnUrl = context.Request.Query["returnUrl"].ToString() ?? "/";

            try
            {
                await mediator.Send(new GoogleLoginCommand(context, returnUrl));
            }
            catch (Exception ex)
            {
                return Results.Redirect("/login?error=google_auth_failed");
            }

            return Results.Redirect(returnUrl);
        });


        app.MapPost("auth/register", async ([FromBody] RegisterBuyerCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithTags("Authentication")
        .WithName("RegisterBuyer")
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
        .WithTags("Authentication")
        .WithDescription("Retrieves information about the currently authenticated user. If the user is not authenticated, returns a 401 Unauthorized status. If the user is not found, returns a 404 Not Found status.");

        app.MapPost("auth/switch-mode", async (SwitchModeCommand command, IMediator mediator) =>
            {
            await mediator.Send(command);

            return Results.NoContent();
        })
        .WithName("SwitchMode")
        .WithTags("Authentication")
        .WithDescription("Switches the mode of the currently authenticated user. If the user is not authenticated, returns a 401 Unauthorized status. This endpoint allows users to switch between different modes (e.g., buyer/seller).");

        app.MapPost("auth/refresh-token", async (HttpContext httpContext, IMediator mediator) =>
        {
            var refreshToken = httpContext.Request.Cookies["REFRESH_TOKEN"];

            var result = await mediator.Send(new RefreshTokenCommand(refreshToken));

            return Results.Ok(result);
        })
         .WithName("RefreshToken")
         .WithTags("Authentication")
         .WithDescription("Refreshes the JWT token using a valid refresh token stored in a cookie or sent in the request body.");

        app.MapGet("/auth/verify-email", async ([AsParameters] VerifyEmailCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Redirect("http://localhost:5173/buyer");
        })
        .WithName("VerifyEmail")
        .WithTags("Authentication")
        .WithDescription("Verifies a user's email using userId and token from the query string.");

        app.MapPost("/auth/logout", async ([FromBody] LogoutCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);

            return Results.NoContent();
        })
        .WithName("Logout")
        .WithTags("Authentication")
        .WithDescription("Logs out the user by clearing the authentication cookies.");

        app.MapPost("/auth/change-password", async([FromBody] ChangePasswordCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("ChangePassword")
        .WithTags("Authentication")
        .WithDescription("Changes the password for the currently authenticated user. Requires the user to be authenticated and provides the new password in the request body.");
    }
}
