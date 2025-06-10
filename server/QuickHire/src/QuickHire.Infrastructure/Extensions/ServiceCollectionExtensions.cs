using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Users.Enums;
using QuickHire.Infrastructure.Authentication;
using QuickHire.Infrastructure.Authentication.Processors;
using QuickHire.Infrastructure.CloudStorage;
using QuickHire.Infrastructure.Communication;
using QuickHire.Infrastructure.Factories.Notification;
using QuickHire.Infrastructure.Helpers;
using QuickHire.Infrastructure.Options;
using QuickHire.Infrastructure.Persistence;
using QuickHire.Infrastructure.Persistence.Identity;
using QuickHire.Infrastructure.Persistence.Repositories;
using QuickHire.Infrastructure.Realtime.Services;
using QuickHire.Infrastructure.Services;
using System.Text;

namespace QuickHire.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection AddAppPersitance(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }

    private static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
        })
.AddCookie(options =>
{
    options.Cookie.Name = "Google_Cookie";
    options.SlidingExpiration = true;
    options.Cookie.SameSite = SameSiteMode.None; 
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
})
.AddJwtBearer(options =>
{
    var section = configuration.GetSection(JwtOptions.JwtOptionsKey);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = section["Issuer"],
        ValidAudience = section["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section["Secret"])),
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Cookies["ACCESS_TOKEN"];

            var path = context.HttpContext.Request.Path;
            if (string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/chathub"))
            {
                accessToken = context.Request.Query["access_token"];
            }

            context.Token = accessToken;

            return Task.CompletedTask;
        }
    };
})
.AddGoogle(options =>
{
    options.ClientId = configuration["GoogleAuthenticationOptions:ClientId"];
    options.ClientSecret = configuration["GoogleAuthenticationOptions:ClientSecret"];
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
    options.SaveTokens = true;
    options.CallbackPath = "/google-redirect";

    options.CorrelationCookie.SameSite = SameSiteMode.None;
    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;

});

        return services;
    }

    private static IServiceCollection AddAppIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAppPersitance(configuration)
            .AddAppIdentity()
            .AddOptions(configuration)
            .AddRepository()
            .AddAppAuthentication(configuration)
            .AddNotificationFactory()
            .AddServices()
            .AddRealtime();
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository>();

        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudinaryOptions>(options => configuration.GetSection(CloudinaryOptions.CloudinaryOptionsKey).Bind(options)); 
        services.Configure<SendGridOptions>(options => configuration.GetSection(SendGridOptions.SendGridOptionsKey).Bind(options));
        services.Configure<JwtOptions>(options => configuration.GetSection(JwtOptions.JwtOptionsKey).Bind(options));
      
        return services;
    }

    private static IServiceCollection AddNotificationFactory(this IServiceCollection services)
    {
        services.TryAddScoped<NewProjectBriefMadeNotificationGenerator>();
        services.AddScoped<CustomOfferReceivedNotificationGenerator>();
        services.AddScoped<CustomOfferAcceptedNotificationGenerator>();
        services.AddScoped<CustomOfferCancelledNotificationGenerator>();
        services.AddScoped<CustomOfferExpiredNotificationGenerator>();
        services.AddScoped<CustomRequestPlacedNotificationGenerator>();
        services.AddScoped<CustomRequestReceivedNotificationGenerator>();
        services.AddScoped<HotGigNotificationGenerator>();
        services.AddScoped<NewGigUploadedNotificationGenerator>();
        services.AddScoped<OrderDeliveredNotificationGenerator>();
        services.AddScoped<OrderPlacedNotificationGenerator>();
        services.AddScoped<OrderStatusUpdateNotificationGenerator>();
        services.AddScoped<ProfileMadeNotificationGenerator>();
        services.AddScoped<ProfileUpdateNotificationGenerator>();
        services.AddScoped<ProjectBriefReceivedNotificationGenerator>();
        services.AddScoped<RevisionReceivedNotificationGenerator>();
        services.AddScoped<ReportedUserNotificationGenerator>();
        services.AddScoped<ReportedGigNotificationGEnerator>();

        services.TryAddScoped<INotificationGeneratorFactory>(provider =>
        {
            var notificationGenerators = new Dictionary<NotificationType, INotificationGenerator>
        {
            { NotificationType.NewProjectBriefMade, provider.GetRequiredService<NewProjectBriefMadeNotificationGenerator>() },
                        { NotificationType.ReportedUser, provider.GetRequiredService<ReportedUserNotificationGenerator>() },

                                    { NotificationType.ReportedGig, provider.GetRequiredService<ReportedGigNotificationGEnerator>() },

            { NotificationType.CustomOfferReceived, provider.GetRequiredService<CustomOfferReceivedNotificationGenerator>() },
            { NotificationType.CustomOfferAccepted, provider.GetRequiredService<CustomOfferAcceptedNotificationGenerator>() },
            { NotificationType.CustomOfferCancelled, provider.GetRequiredService<CustomOfferCancelledNotificationGenerator>() },
            { NotificationType.CustomOfferExpired, provider.GetRequiredService<CustomOfferExpiredNotificationGenerator>() },
            { NotificationType.CustomRequestPlaced, provider.GetRequiredService<CustomRequestPlacedNotificationGenerator>() },
            { NotificationType.CustomRequestReceived, provider.GetRequiredService<CustomRequestReceivedNotificationGenerator>() },
            { NotificationType.HotGig, provider.GetRequiredService<HotGigNotificationGenerator>() },
            { NotificationType.NewGigUploaded, provider.GetRequiredService<NewGigUploadedNotificationGenerator>() },
            { NotificationType.OrderDelivered, provider.GetRequiredService<OrderDeliveredNotificationGenerator>() },
            { NotificationType.OrderPlaced, provider.GetRequiredService<OrderPlacedNotificationGenerator>() },
            { NotificationType.OrderStatusUpdate, provider.GetRequiredService<OrderStatusUpdateNotificationGenerator>() },
            { NotificationType.ProfileMade, provider.GetRequiredService<ProfileMadeNotificationGenerator>() },
            { NotificationType.ProfileUpdate, provider.GetRequiredService<ProfileUpdateNotificationGenerator>() },
            { NotificationType.ProjectBriefReceived, provider.GetRequiredService<ProjectBriefReceivedNotificationGenerator>() },
            { NotificationType.RevisionReceived, provider.GetRequiredService<RevisionReceivedNotificationGenerator>() },
        };

            return new NotificationGenerationFactory(notificationGenerators);
        });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();
        services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IPdfHelper, PdfHelper>();
        services.AddScoped<IGigScoringService, GigScoringService>();

        return services;
    }

    private static IServiceCollection AddRealtime(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }
}
