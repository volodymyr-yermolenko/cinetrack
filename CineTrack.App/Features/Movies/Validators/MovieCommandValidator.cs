using CineTrack.App.Common;
using CineTrack.App.Exceptions;
using CineTrack.App.Extentions;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.Movies.Validators;

public class MovieCommandValidator(IMovieRepository repository, IGenreRepository genreRepository)
{
    public async Task ValidateMovieCreationAsync(int userId, IMovieAttributes movie)
    {
        if (await repository.MovieExistsAsync(movie.Title, movie.ReleaseYear, userId)) 
        {
            throw new AppValidationException(ErrorMessages.DuplicateMovie);
        }
        await ValidateMovieAsync(movie);
    }

    public async Task ValidateMovieUpdateAsync(int userId, int movieId, IMovieAttributes movie)
    {
        if (await repository.MovieExistsAsync(movie.Title, movie.ReleaseYear, userId, movieId)) 
        {
            throw new AppValidationException(ErrorMessages.DuplicateMovie);
        }
        await ValidateMovieAsync(movie);
    }

    private async Task ValidateMovieAsync(IMovieAttributes movie)
    {
        if (movie.GenreIds.Count == 0)
        {
            throw new AppValidationException(ErrorMessages.MovieMustHaveGenres);
        }
        if (!await genreRepository.AllGenresExistAsync(movie.GenreIds))
        {
            throw new AppValidationException(ErrorMessages.SomeGenresNotExist);
        }
        if (!movie.MovieType.IsValidEnum())
        {
            throw new AppValidationException(ErrorMessages.InvalidMovieType);
        }
        if (movie.ReleaseYear > DateTime.UtcNow.Year) 
        {
            throw new AppValidationException(ErrorMessages.ReleaseYearInFuture);
        }
        if (movie.ReleaseYear < 1900) 
        {
            throw new AppValidationException(ErrorMessages.ReleaseYearBefore1900);
        }
        if (movie.ImageUrl != null && !Uri.IsWellFormedUriString(movie.ImageUrl, UriKind.Absolute)) 
        {
            throw new AppValidationException(ErrorMessages.InvalidImageUrl);
        }
    }
}