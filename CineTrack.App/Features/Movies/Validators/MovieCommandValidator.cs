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
            throw new AppValidationException("Movie with the same title and release year already exists");
        }
        ValidateMovie(movie);
    }

    public async Task ValidateMovieUpdateAsync(int userId, int movieId, IMovieAttributes movie)
    {
        if (await repository.MovieExistsAsync(movie.Title, movie.ReleaseYear, userId, movieId)) 
        {
            throw new AppValidationException("Movie with the same title and release year already exists");
        }
        ValidateMovie(movie);
    }

    private static void ValidateMovie(IMovieAttributes movie)
    {
        if (!movie.MovieType.IsValidEnum())
        {
            throw new AppValidationException("Invalid movie type");
        }
        if (movie.ReleaseYear > DateTime.Now.Year) 
        {
            throw new  AppValidationException("Release year cannot be in the future");
        }
        if (movie.ReleaseYear < 1900) 
        {
            throw new AppValidationException("Release year cannot be before 1900");
        }
        if (movie.ImageUrl != null && !Uri.IsWellFormedUriString(movie.ImageUrl, UriKind.Absolute)) 
        {
            throw new AppValidationException("Invalid image URL");
        }
    }
}