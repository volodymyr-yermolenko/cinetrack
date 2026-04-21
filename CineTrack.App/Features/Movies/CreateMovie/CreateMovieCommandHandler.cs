using AutoMapper;
using MediatR;
using CineTrack.App.Features.Movies.Validators;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;

namespace CineTrack.App.Features.Movies.CreateMovie;

public class CreateMovieCommandHandler(
    IMovieRepository repository, 
    IGenreRepository genreRepository,
    IMapper mapper,
    MovieCommandValidator validator) 
    : IRequestHandler<CreateMovieCommand, int>
{
    public async Task<int> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        command.Movie.Title = command.Movie.Title.Trim();
        await validator.ValidateMovieCreationAsync(command.UserId, command.Movie);

        var movieDto = command.Movie;
        var movie = mapper.Map<Movie>(movieDto);
        movie.UserId = command.UserId;

        var genres = await genreRepository.GetGenresByIdsAsync(movieDto.GenreIds);
        movie.Genres = genres;
        
        var now = DateTime.UtcNow;
        movie.CreatedAt = now;
        movie.UpdatedAt = now;
        
        await repository.AddMovieAsync(movie);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return movie.Id;
    }
}