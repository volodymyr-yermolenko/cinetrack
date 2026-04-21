using MediatR;

namespace CineTrack.App.Features.Movies.DeleteMovie;

public class DeleteMovieCommand(int userId, int movieId) : IRequest<Unit>
{
    public int UserId { get; } = userId;
    public int MovieId { get; } = movieId;
}