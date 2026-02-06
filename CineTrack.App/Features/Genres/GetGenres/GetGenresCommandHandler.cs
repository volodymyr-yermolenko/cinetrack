using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using MediatR;

namespace CineTrack.App.Features.Genres.GetGenres;

public class GetGenresCommandHandler(IGenreRepository repository) : IRequestHandler<GetGenresCommand, List<Genre>>
{
    public Task<List<Genre>> Handle(GetGenresCommand request, CancellationToken cancellationToken)
    {
        return repository.GetGenresAsync();
    }
}