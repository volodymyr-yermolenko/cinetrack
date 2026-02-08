using AutoMapper;
using CineTrack.App.Exceptions;
using CineTrack.App.Interfaces;
using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.Movies.GetMovie;

public class GetMovieRequestHandler(IMovieRepository repository, IMapper mapper) : IRequestHandler<GetMovieRequest, MovieDto>
{
    public async Task<MovieDto> Handle(GetMovieRequest request, CancellationToken cancellationToken)
    {
        var movie = await repository.GetMovieAsync(request.MovieId);
        if (movie == null)
        {
            throw new AppNotFoundException("Movie not found");
        }
        if (movie.UserId != request.UserId)
        {
            throw new AppForbiddenException("Unauthorized access");
        }

        return mapper.Map<MovieDto>(movie);
    }
}