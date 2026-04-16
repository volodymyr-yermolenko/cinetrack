using CineTrack.App.Models.Movies;
using MediatR;

namespace CineTrack.App.Features.Movies.CreateMovie;

public class CreateMovieCommand(int userId) : IRequest<int>
{
    public int UserId { get; } = userId;
    public required CreateMovieDto Movie { get; init; }
}