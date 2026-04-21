namespace CineTrack.App.Models.Authentication;

public enum EmailConfirmationResult
{
    Success = 0,
    InvalidToken = 1,
    TokenExpired = 2
}