using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.Movies.UpdateMovie;

public class UpdateMovieCommand(int userId) : IRequest<Unit>
{
    public int UserId { get; } = userId;
    public required int MovieId { get; init; }
    public required UpdateMovieDto Movie { get; init; }
}