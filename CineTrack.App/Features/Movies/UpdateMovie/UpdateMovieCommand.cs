using MediatR;
using CineTrack.App.Models.Movies;

namespace CineTrack.App.Features.Movies.UpdateMovie;

public class UpdateMovieCommand(int userId, int movieId, UpdateMovieDto movie) : IRequest<Unit>
{
    public int UserId { get; } = userId;
    public int MovieId { get; } = movieId;
    public UpdateMovieDto Movie { get; } = movie;
}