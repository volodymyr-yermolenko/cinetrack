using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Extensions;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.Movies.Validators;

public class MovieCommandValidator(IMovieRepository repository, IGenreRepository genreRepository)
{
    public async Task ValidateMovieCreationAsync(int userId, IMovieAttributes movie)
    {
        await ValidateMovieAsync(movie);
        if (await repository.MovieExistsAsync(movie.Title, movie.ReleaseYear, userId)) 
        {
            throw new AppValidationException(MovieErrorMessages.DuplicateMovie);
        }
    }

    public async Task ValidateMovieUpdateAsync(int userId, int movieId, IMovieAttributes movie)
    {
        if (await repository.MovieExistsAsync(movie.Title, movie.ReleaseYear, userId, movieId)) 
        {
            throw new AppValidationException(MovieErrorMessages.DuplicateMovie);
        }
        await ValidateMovieAsync(movie);
    }

    private async Task ValidateMovieAsync(IMovieAttributes movie)
    {
        if (string.IsNullOrWhiteSpace(movie.Title))
        {
            throw new AppValidationException(MovieErrorMessages.TitleRequired);
        }
        if (movie.GenreIds.Count == 0)
        {
            throw new AppValidationException(MovieErrorMessages.MovieMustHaveGenres);
        }
        if (!await genreRepository.AllGenresExistAsync(movie.GenreIds))
        {
            throw new AppValidationException(MovieErrorMessages.SomeGenresNotExist);
        }
        if (!movie.MovieType.IsValidEnum())
        {
            throw new AppValidationException(MovieErrorMessages.InvalidMovieType);
        }
        if (movie.ReleaseYear > DateTime.UtcNow.Year) 
        {
            throw new AppValidationException(MovieErrorMessages.ReleaseYearInFuture);
        }
        if (movie.ReleaseYear < 1900) 
        {
            throw new AppValidationException(MovieErrorMessages.ReleaseYearBefore1900);
        }
        if (movie.ImageUrl != null && !Uri.IsWellFormedUriString(movie.ImageUrl, UriKind.Absolute)) 
        {
            throw new AppValidationException(MovieErrorMessages.InvalidImageUrl);
        }
    }
}