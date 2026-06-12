namespace CineTrack.App.Models.Authentication;

public enum ResetPasswordStatus
{
    Success = 0,
    InvalidToken = 1,
    TokenExpired = 2
}
