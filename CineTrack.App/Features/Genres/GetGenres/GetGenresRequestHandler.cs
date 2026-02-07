using AutoMapper;
using CineTrack.App.Interfaces;
using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.Genres.GetGenres;

public class GetGenresRequestHandler(IGenreRepository repository, IMapper mapper) 
    : IRequestHandler<GetGenresRequest, List<GenreDto>>
{
    public async Task<List<GenreDto>> Handle(GetGenresRequest request, CancellationToken cancellationToken)
    {
        var genres = await repository.GetGenresAsync();
        return mapper.Map<List<GenreDto>>(genres);
    }
}