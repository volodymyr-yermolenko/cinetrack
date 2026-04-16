namespace CineTrack.App.Models;

public enum EmailConfirmationResult
{
    Success = 0,
    UserNotFound = 1,
    TokenExpired = 2
}