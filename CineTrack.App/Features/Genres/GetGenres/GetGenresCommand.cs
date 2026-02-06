using CineTrack.Domain.Entities;
using MediatR;

namespace CineTrack.App.Features.Genres.GetGenres;

public class GetGenresCommand : IRequest<List<Genre>>
{
}