using AutoMapper;
using MediatR;
using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Features.WatchEntries.Validators;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.WatchEntries.UpdateWatchEntry;

public class UpdateWatchEntryCommandHandler(IWatchEntryRepository repository, IMapper mapper, WatchEntryCommandValidator validator)
    : IRequestHandler<UpdateWatchEntryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateWatchEntryCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateWatchEntryUpdateAsync(command.UserId, command.WatchEntryId, command.WatchEntry);

        var watchEntryDto = command.WatchEntry;
        var watchEntry = await repository.GetWatchEntryAsync(command.UserId, command.WatchEntryId);
        if (watchEntry == null)
        {
            throw new AppNotFoundException(WatchEntryErrorMessages.WatchEntryNotFound);
        }
        
        mapper.Map(watchEntryDto, watchEntry);
        watchEntry.UpdatedAt = DateTime.UtcNow;
        
        repository.UpdateWatchEntry(watchEntry);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return Unit.Value;
    }
}