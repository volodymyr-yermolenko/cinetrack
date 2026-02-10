using CineTrack.App.Common;
using CineTrack.App.Exceptions;
using CineTrack.App.Extentions;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.Movies.Validators;

public class MovieCommandValidator(IMovieRepository repository)
{
    public async Task ValidateMovieCreationAsync(int userId, IMovieAttributes movie)
    {
        if (await repository.MovieExistsAsync(movie.Title, movie.ReleaseYear, userId)) 
        {
            throw new AppValidationException(ErrorMessages.DuplicateMovie);
        }
        ValidateMovie(movie);
    }

    public async Task ValidateMovieUpdateAsync(int userId, int movieId, IMovieAttributes movie)
    {
        if (await repository.MovieExistsAsync(movie.Title, movie.ReleaseYear, userId, movieId)) 
        {
            throw new AppValidationException(ErrorMessages.DuplicateMovie);
        }
        ValidateMovie(movie);
    }

    private static void ValidateMovie(IMovieAttributes movie)
    {
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