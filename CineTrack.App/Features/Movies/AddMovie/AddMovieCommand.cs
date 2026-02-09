using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.Movies.AddMovie;

public class AddMovieCommand(int userId) : IRequest<int>
{
    public int UserId { get; } = userId;
    public required AddMovieDto Movie { get; init; }
}