using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.GetCurrentUser;

public class GetCurrentUserRequest(int userId) : IRequest<UserDto>
{
    public int UserId { get; } = userId;
}