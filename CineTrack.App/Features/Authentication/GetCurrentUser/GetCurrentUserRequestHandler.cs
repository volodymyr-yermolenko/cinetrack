using MediatR;
using AutoMapper;
using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.GetCurrentUser;

public class GetCurrentUserRequestHandler(IUserRepository userRepository, IMapper mapper) 
    : IRequestHandler<GetCurrentUserRequest, UserDto>
{
    public async Task<UserDto> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new AppNotFoundException(AuthErrorMessages.UserNotFound);
        }

        return mapper.Map<UserDto>(user);
    }
}