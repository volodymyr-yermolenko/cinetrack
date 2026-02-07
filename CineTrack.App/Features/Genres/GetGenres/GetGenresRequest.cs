using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.Genres.GetGenres;

public class GetGenresRequest : IRequest<List<GenreDto>>
{
}