using CineTrack.App.Features.Genres.GetGenres;
using CineTrack.App.Interfaces;
using CineTrack.Infrastructure.Repositories;

namespace CineTrack.Api;

public static class ServiceRegistrations
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetGenresCommandHandler).Assembly));
        services.AddScoped<IGenreRepository, GenreRepository>();
        return services;
    }
}