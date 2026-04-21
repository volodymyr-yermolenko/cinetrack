using MediatR;
using CineTrack.App.Models.Movies;

namespace CineTrack.App.Features.Movies.CreateMovie;

public class CreateMovieCommand(int userId, CreateMovieDto movie) : IRequest<int>
{
    public int UserId { get; } = userId;
    public CreateMovieDto Movie { get; } = movie;
}