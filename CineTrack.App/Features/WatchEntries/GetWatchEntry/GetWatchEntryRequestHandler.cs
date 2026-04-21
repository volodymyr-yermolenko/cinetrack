using AutoMapper;
using MediatR;
using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.WatchEntries;

namespace CineTrack.App.Features.WatchEntries.GetWatchEntry;

public class GetWatchEntryRequestHandler(IWatchEntryRepository repository, IMapper mapper) 
    : IRequestHandler<GetWatchEntryRequest, WatchEntryDto>
{
    public async Task<WatchEntryDto> Handle(GetWatchEntryRequest request, CancellationToken cancellationToken)
    {
        var entry = await repository.GetWatchEntryAsync(request.UserId, request.WatchEntryId);
        if (entry == null)
        {
            throw new AppNotFoundException(WatchEntryErrorMessages.WatchEntryNotFound);
        }
        
        return mapper.Map<WatchEntryDto>(entry);
    }
}