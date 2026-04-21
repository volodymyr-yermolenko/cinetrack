namespace CineTrack.App.Common.Constants;

public static class MovieErrorMessages
{
    public const string TitleRequired = "Movie title is required";
    public const string MovieNotFound = "Movie with provided ID is not found";
    public const string DuplicateMovie = "Movie with the same title and release year already exists";
    public const string InvalidMovieType = "Invalid movie type";
    public const string ReleaseYearInFuture = "Release year cannot be in the future";
    public const string ReleaseYearBefore1900 = "Release year cannot be before 1900";
    public const string InvalidImageUrl = "Invalid image URL";
    public const string SomeGenresNotExist = "Some of the provided genre IDs do not exist";
    public const string MovieMustHaveGenres = "Movie must have at least one genre";
}

public static class WatchEntryErrorMessages
{
    public const string WatchEntryNotFound = "Watch entry with provided ID is not found";
    public const string DuplicateWatchEntry = "Watch entry for the same movie and watching date already exists";
    public const string InvalidViewingContext = "Invalid viewing context value";
    public const string WatchedDateInFuture = "Watched date cannot be in the future";
    public const string WatchedDateBeforeRelease = "Watched date cannot be before movie release year";
    public const string InvalidRating = "Rating must be between 1 and 10";
}

public static class AuthErrorMessages
{
    public const string EmailRequired = "Email is required";
    public const string InvalidEmail = "Email is invalid";
    public const string PasswordRequired = "Password is required";
    public const string PasswordContainsOuterSpaces = "Password cannot start or end with spaces";
    public const string InvalidPassword = "Password must be at least 8 characters long and contain uppercase, lowercase letters, and numbers";
    public const string UserNameRequired = "User name is required";
}