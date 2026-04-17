using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface ITokenService
{
    string? GenerateAccessToken(User user);
}