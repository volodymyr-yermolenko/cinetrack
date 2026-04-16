using MediatR;
using CineTrack.App.Models.Movies;

namespace CineTrack.App.Features.Movies.GetMovie;

public class GetMovieRequest(int userId) : IRequest<MovieDto>
{
    public int UserId { get; } = userId;
    public int MovieId { get; init; }
}