using CineTrack.Api.Middlewares;
using CineTrack.App;
using CineTrack.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseErrorHandlingMiddleware();
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response is { StatusCode: StatusCodes.Status401Unauthorized, HasStarted: false })
    {
        response.ContentType = "application/json";
        await response.WriteAsJsonAsync(new { error = "Unauthorized access" });
    }
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();