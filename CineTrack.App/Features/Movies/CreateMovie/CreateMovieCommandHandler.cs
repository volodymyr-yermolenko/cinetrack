using AutoMapper;
using CineTrack.App.Features.Movies.Validators;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using MediatR;

namespace CineTrack.App.Features.Movies.CreateMovie;

public class CreateMovieCommandHandler(IMovieRepository repository, IMapper mapper, MovieCommandValidator validator) 
    : IRequestHandler<CreateMovieCommand, int>
{
    public async Task<int> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateMovieCreationAsync(command.UserId, command.Movie);
        
        var movie = mapper.Map<Movie>(command.Movie);
        movie.UserId = command.UserId;
        
        var now = DateTime.UtcNow;
        movie.CreatedAt = now;
        movie.UpdatedAt = now;
        
        await repository.AddMovieAsync(movie);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return movie.Id;
    }
}