using Microsoft.Extensions.DependencyInjection;
using CineTrack.App.Features.Movies.Validators;
using CineTrack.App.Features.WatchEntries.Validators;
using CineTrack.App.Services;

namespace CineTrack.App;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var thisAssembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(thisAssembly));
        services.AddAutoMapper(cfg => cfg.AddMaps(thisAssembly));

        services.AddScoped<MovieCommandValidator>();
        services.AddScoped<WatchEntryCommandValidator>();
        services.AddScoped<EmailConfirmationService>();
        services.AddScoped<PasswordResetService>();

        return services;
    }
}