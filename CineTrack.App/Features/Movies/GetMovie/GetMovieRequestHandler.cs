using AutoMapper;
using MediatR;
using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.Movies;

namespace CineTrack.App.Features.Movies.GetMovie;

public class GetMovieRequestHandler(IMovieRepository repository, IMapper mapper) : IRequestHandler<GetMovieRequest, MovieDto>
{
    public async Task<MovieDto> Handle(GetMovieRequest request, CancellationToken cancellationToken)
    {
        var movie = await repository.GetMovieAsync(request.UserId, request.MovieId);
        if (movie == null)
        {
            throw new AppNotFoundException(ErrorMessages.MovieNotFound);
        }

        return mapper.Map<MovieDto>(movie);
    }
}