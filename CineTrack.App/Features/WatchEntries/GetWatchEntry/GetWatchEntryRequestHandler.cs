using AutoMapper;
using CineTrack.App.Common;
using CineTrack.App.Exceptions;
using CineTrack.App.Interfaces;
using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.WatchEntries.GetWatchEntry;

public class GetWatchEntryRequestHandler(IWatchEntryRepository repository, IMapper mapper) 
    : IRequestHandler<GetWatchEntryRequest, WatchEntryDto>
{
    public async Task<WatchEntryDto> Handle(GetWatchEntryRequest request, CancellationToken cancellationToken)
    {
        var entry = await repository.GetWatchEntryAsync(request.UserId, request.WatchEntryId);
        if (entry == null)
        {
            throw new AppNotFoundException(ErrorMessages.WatchEntryNotFound);
        }
        
        return mapper.Map<WatchEntryDto>(entry);
    }
}