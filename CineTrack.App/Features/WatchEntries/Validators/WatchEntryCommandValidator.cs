using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Extensions;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.WatchEntries.Validators;

public class WatchEntryCommandValidator(IWatchEntryRepository repository, IMovieRepository movieRepository)
{
    public async Task ValidateWatchEntryCreationAsync(int userId, IWatchEntryAttributes watchEntry)
    {
        if (await repository.WatchEntryExistsAsync(userId, watchEntry.MovieId, watchEntry.WatchedDate)) 
        {
            throw new AppValidationException(WatchEntryErrorMessages.DuplicateWatchEntry);
        }
        await ValidateWatchEntryAsync(userId, watchEntry);
    }

    public async Task ValidateWatchEntryUpdateAsync(int userId, int watchEntryId, IWatchEntryAttributes watchEntry)
    {
        if (await repository.WatchEntryExistsAsync(userId, watchEntry.MovieId, watchEntry.WatchedDate, watchEntryId)) 
        {
            throw new AppValidationException(WatchEntryErrorMessages.DuplicateWatchEntry);
        }
        await ValidateWatchEntryAsync(userId, watchEntry);
    }

    private async Task ValidateWatchEntryAsync(int userId, IWatchEntryAttributes watchEntry)
    {
        var movie = await movieRepository.GetMovieAsync(userId, watchEntry.MovieId);
        if (movie == null)
        {
            throw new AppValidationException(MovieErrorMessages.MovieNotFound);
        }
        if (!watchEntry.ViewingContext.IsValidEnum())
        {
            throw new AppValidationException(WatchEntryErrorMessages.InvalidViewingContext);
        }
        if (watchEntry.WatchedDate > DateTime.UtcNow) 
        {
            throw new  AppValidationException(WatchEntryErrorMessages.WatchedDateInFuture);
        }
        if (watchEntry.WatchedDate.Year < movie.ReleaseYear) 
        {
            throw new AppValidationException(WatchEntryErrorMessages.WatchedDateBeforeRelease);
        }
        if (watchEntry.Rating is < 1 or > 10) 
        {
            throw new AppValidationException(WatchEntryErrorMessages.InvalidRating);
        }
    }    
}