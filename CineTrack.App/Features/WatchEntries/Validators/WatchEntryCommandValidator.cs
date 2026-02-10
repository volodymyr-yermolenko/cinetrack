using CineTrack.App.Common;
using CineTrack.App.Exceptions;
using CineTrack.App.Extentions;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.WatchEntries.Validators;

public class WatchEntryCommandValidator(IWatchEntryRepository repository, IMovieRepository movieRepository)
{
    public async Task ValidateWatchEntryCreationAsync(int userId, IWatchEntryAttributes watchEntry)
    {
        if (await repository.WatchEntryExistsAsync(userId, watchEntry.MovieId, watchEntry.WatchedAt)) 
        {
            throw new AppValidationException(ErrorMessages.DuplicateWatchEntry);
        }
        await ValidateWatchEntryAsync(userId, watchEntry);
    }

    public async Task ValidateWatchEntryUpdateAsync(int userId, int watchEntryId, IWatchEntryAttributes watchEntry)
    {
        if (await repository.WatchEntryExistsAsync(userId, watchEntry.MovieId, watchEntry.WatchedAt, watchEntryId)) 
        {
            throw new AppValidationException(ErrorMessages.DuplicateWatchEntry);
        }
        await ValidateWatchEntryAsync(userId, watchEntry);
    }

    private async Task ValidateWatchEntryAsync(int userId, IWatchEntryAttributes watchEntry)
    {
        var movie = await movieRepository.GetMovieAsync(userId, watchEntry.MovieId);
        if (movie == null)
        {
            throw new AppValidationException(ErrorMessages.MovieNotFound);
        }
        if (!watchEntry.ViewingContext.IsValidEnum())
        {
            throw new AppValidationException(ErrorMessages.InvalidViewingContext);
        }
        if (watchEntry.WatchedAt > DateTime.UtcNow) 
        {
            throw new  AppValidationException(ErrorMessages.WatchedDateInFuture);
        }
        if (watchEntry.WatchedAt.Year < movie.ReleaseYear) 
        {
            throw new AppValidationException(ErrorMessages.WatchedDateBeforeRelease);
        }
        if (watchEntry.Rating is < 1 or > 10) 
        {
            throw new AppValidationException(ErrorMessages.InvalidRating);
        }
    }    
}