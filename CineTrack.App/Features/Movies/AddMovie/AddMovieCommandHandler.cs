using AutoMapper;
using CineTrack.App.Features.Movies.Validators;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using MediatR;

namespace CineTrack.App.Features.Movies.AddMovie;

public class AddMovieCommandHandler(IMovieRepository repository, IMapper mapper, MovieCommandValidator validator) 
    : IRequestHandler<AddMovieCommand, int>
{
    public async Task<int> Handle(AddMovieCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateMovieCreationAsync(command.UserId, command.Movie);
        
        var movie = mapper.Map<Movie>(command.Movie);
        movie.UserId = command.UserId;
        movie.CreatedAt = DateTime.Now;
        movie.UpdatedAt = DateTime.Now;
        
        await repository.AddMovieAsync(movie);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return movie.Id;
    }
}