using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using QuickHire.Api.Exceptions;
using QuickHire.Api.Extensions;
using QuickHire.Application.Admin.MainCategories.AddMainCategory;
using QuickHire.Application.Common.Behaviors;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Authentication.Login;
using QuickHire.Application.Users.Authentication.RefreshToken;
using QuickHire.Application.Users.Authentication.Register;
using QuickHire.Application.Users.Authentication.VerifyEmail;
using QuickHire.Infrastructure.Options;
using QuickHire.Infrastructure.Persistence.Seed;
using QuickHire.Infrastructure.Realtime.Hubs;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;
builder.Services.AddValidatorsFromAssembly(typeof(AddMainCategoryCommandValidator).Assembly);

var applicationAssembly = typeof(ICommandHandler<,>).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(applicationAssembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSignalR();
builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterApplication(assembly);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<CustomExceptionHandling>();

builder.Services.Configure<GoogleAuthenticationOptions>(options => builder.Configuration.GetSection(GoogleAuthenticationOptions.GoogleAuthenticationOptionsKey).Bind(options));

var serviceProvider = builder.Services.BuildServiceProvider();
var googleAuthenticationOptions = serviceProvider.GetRequiredService<IOptions<GoogleAuthenticationOptions>>().Value;

builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = googleAuthenticationOptions.ClientId;
    options.ClientSecret = googleAuthenticationOptions.ClientSecret;
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
    options.SaveTokens = true; 
});

builder.Services.AddAntiforgery(options =>
{
    options.SuppressXFrameOptionsHeader = true;
});

builder.Services.AddScoped<SeedData>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var seeder = services.GetRequiredService<SeedData>();
        await seeder.SeedAsync(); 
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error during seeding: " + ex.Message);
    }
}

app.Use(async (context, next) =>
{
    context.Request.Headers.Remove("X-XSRF-TOKEN");
    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthentication();    
app.UseAuthorization();

app.MapCarter();
app.UseCors("AllowLocalhost");



app.Run();

/*Add-Migration Test -Project "src\QuickHire.Infrastructure" -StartupProject "src\QuickHire.Api"*/


