namespace CineTrack.App.Common;

public static class ErrorMessages
{
    public const string MovieNotFound = "Movie with provided ID is not found";
    public const string DuplicateMovie = "Movie with the same title and release year already exists";
    public const string InvalidMovieType = "Invalid movie type";
    public const string ReleaseYearInFuture = "Release year cannot be in the future";
    public const string ReleaseYearBefore1900 = "Release year cannot be before 1900";
    public const string InvalidImageUrl = "Invalid image URL";
    
    public const string WatchEntryNotFound = "Watch entry with provided ID is not found";
    public const string DuplicateWatchEntry = "Watch entry for the same movie and watching date already exists";
    public const string InvalidViewingContext = "Invalid viewing context value";
    public const string WatchedDateInFuture = "Watched date cannot be in the future";
    public const string WatchedDateBeforeRelease = "Watched date cannot be before movie release year";
    public const string InvalidRating = "Rating must be between 1 and 10";
}