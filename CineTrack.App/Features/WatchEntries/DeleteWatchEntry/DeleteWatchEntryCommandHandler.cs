using CineTrack.App.Common;
using CineTrack.App.Exceptions;
using CineTrack.App.Interfaces;
using MediatR;

namespace CineTrack.App.Features.WatchEntries.DeleteWatchEntry;

public class DeleteWatchEntryCommandHandler(IWatchEntryRepository repository) 
    : IRequestHandler<DeleteWatchEntryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteWatchEntryCommand request, CancellationToken cancellationToken)
    {
        var watchEntry = await repository.GetWatchEntryAsync(request.UserId, request.WatchEntryId);
        if (watchEntry == null)
        {
            throw new AppNotFoundException(ErrorMessages.WatchEntryNotFound);
        }
        
        repository.DeleteWatchEntry(watchEntry);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return Unit.Value;
    }
}