using AutoMapper;
using CineTrack.App.Features.WatchEntries.Validators;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using MediatR;

namespace CineTrack.App.Features.WatchEntries.CreateWatchEntry;

public class CreateWatchEntryCommandHandler(IWatchEntryRepository repository, IMapper mapper, WatchEntryCommandValidator validator)
    : IRequestHandler<CreateWatchEntryCommand, int>
{
    public async Task<int> Handle(CreateWatchEntryCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateWatchEntryCreationAsync(command.UserId, command.WatchEntry);
        
        var watchEntry = mapper.Map<WatchEntry>(command.WatchEntry);
        watchEntry.UserId = command.UserId;
        
        var now = DateTime.UtcNow;
        watchEntry.CreatedAt = now;
        watchEntry.UpdatedAt = now;
        
        await repository.AddWatchEntryAsync(watchEntry);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return watchEntry.Id;
    }
}