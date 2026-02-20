using CineTrack.App.Models;
using CineTrack.Domain.Entities;
using MediatR;

namespace CineTrack.App.Features.Movies.GetMovies;

public class GetMoviesRequest(int userId) : IRequest<List<MovieDto>>
{
    public int UserId { get; } = userId;
    public int? GenreId { get; init; }
    public string? SearchString { get; init; }
}