namespace CineTrack.App.Models.Authentication;

public enum EmailConfirmationResult
{
    Success = 0,
    UserNotFound = 1,
    TokenExpired = 2
}