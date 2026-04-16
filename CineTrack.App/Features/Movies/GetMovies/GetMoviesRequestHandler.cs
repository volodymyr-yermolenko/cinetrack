using AutoMapper;
using MediatR;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.Movies;

namespace CineTrack.App.Features.Movies.GetMovies;

public class GetMoviesRequestHandler(IMovieRepository repository, IMapper mapper) 
    : IRequestHandler<GetMoviesRequest, List<MovieDto>>
{
    public async Task<List<MovieDto>> Handle(GetMoviesRequest request, CancellationToken cancellationToken)
    {
        var movies = await repository.GetMoviesAsync(request.UserId, request.GenreId, request.SearchString);
        return mapper.Map<List<MovieDto>>(movies);
    }
}