using Microsoft.Extensions.DependencyInjection;
using CineTrack.App.Features.Movies.Validators;
using CineTrack.App.Features.WatchEntries.Validators;

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

        return services;
    }
}