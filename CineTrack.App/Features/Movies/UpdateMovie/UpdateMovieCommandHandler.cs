using AutoMapper;
using MediatR;
using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Features.Movies.Validators;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.Movies.UpdateMovie;

public class UpdateMovieCommandHandler(
    IMovieRepository repository, 
    IGenreRepository genreRepository, 
    IMapper mapper, 
    MovieCommandValidator validator) 
    : IRequestHandler<UpdateMovieCommand, Unit>
{
    public async Task<Unit> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateMovieUpdateAsync(command.UserId, command.MovieId, command.Movie);
        
        var movieDto = command.Movie;
        var movie = await repository.GetMovieAsync(command.UserId, command.MovieId);
        if (movie == null)
        {
            throw new AppNotFoundException(ErrorMessages.MovieNotFound);
        }
        
        var genres = await genreRepository.GetGenresByIdsAsync(movieDto.GenreIds);
        movie.Genres = genres;
        
        mapper.Map(movieDto, movie);
        movie.UpdatedAt = DateTime.UtcNow;
        
        repository.UpdateMovie(movie);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return Unit.Value;
    }
}