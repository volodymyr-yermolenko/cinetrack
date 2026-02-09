using AutoMapper;
using CineTrack.App.Exceptions;
using CineTrack.App.Features.Movies.Validators;
using CineTrack.App.Interfaces;
using MediatR;

namespace CineTrack.App.Features.Movies.UpdateMovie;

public class UpdateMovieCommandHandler(IMovieRepository repository, IMapper mapper, MovieCommandValidator validator) 
    : IRequestHandler<UpdateMovieCommand, Unit>
{
    public async Task<Unit> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateMovieUpdateAsync(command.UserId, command.MovieId, command.Movie);
        
        var movieDto = command.Movie;
        var movie = await repository.GetMovieAsync(command.UserId, command.MovieId);
        if (movie == null)
        {
            throw new AppNotFoundException("Movie not found");
        }
        mapper.Map(movieDto, movie);
        movie.UpdatedAt = DateTime.UtcNow;
        
        repository.UpdateMovie(movie);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return Unit.Value;
    }
}