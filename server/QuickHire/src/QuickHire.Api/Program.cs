using Carter;
using QuickHire.Api.Exceptions;
using QuickHire.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
               builder =>
               {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddSignalR();
builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterApplication(assembly);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddExceptionHandler<CustomExceptionHandling>();

var app = builder.Build();

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

app.Run();


