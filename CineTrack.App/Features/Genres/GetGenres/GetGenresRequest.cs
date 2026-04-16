using MediatR;
using CineTrack.App.Models.Genres;

namespace CineTrack.App.Features.Genres.GetGenres;

public class GetGenresRequest : IRequest<List<GenreDto>>
{
}