using MediatR;

namespace CineTrack.App.Features.Movies.DeleteMovie;

public class DeleteMovieCommand(int userId) : IRequest<Unit>
{
    public int UserId { get; } = userId;
    public required int MovieId { get; init; }
}