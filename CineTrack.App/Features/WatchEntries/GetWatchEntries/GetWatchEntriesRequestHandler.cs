using AutoMapper;
using CineTrack.App.Interfaces;
using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.WatchEntries.GetWatchEntries;

public class GetWatchEntriesRequestHandler(IWatchEntryRepository repository, IMapper mapper) 
    : IRequestHandler<GetWatchEntriesRequest, List<WatchEntryDto>>
{
    public async Task<List<WatchEntryDto>> Handle(GetWatchEntriesRequest request, CancellationToken cancellationToken)
    {
        var entries = await repository.GetWatchEntriesAsync(request.UserId, request.GenreId, request.SearchString);
        return mapper.Map<List<WatchEntryDto>>(entries);
    }
}