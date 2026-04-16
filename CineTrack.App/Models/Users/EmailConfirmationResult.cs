namespace CineTrack.App.Models.Users;

public enum EmailConfirmationResult
{
    Success = 0,
    UserNotFound = 1,
    TokenExpired = 2
}