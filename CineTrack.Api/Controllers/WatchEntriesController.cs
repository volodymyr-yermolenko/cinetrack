using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CineTrack.App.Features.WatchEntries.CreateWatchEntry;
using CineTrack.App.Features.WatchEntries.DeleteWatchEntry;
using CineTrack.App.Features.WatchEntries.GetWatchEntries;
using CineTrack.App.Features.WatchEntries.GetWatchEntry;
using CineTrack.App.Features.WatchEntries.UpdateWatchEntry;
using CineTrack.App.Models.WatchEntries;

namespace CineTrack.Api.Controllers;

[ApiController]
//[Authorize]
[Route("api/watch-entries")]
public class WatchEntriesController(IMediator mediator) : BaseController
{
    [HttpGet("")]
    [ProducesResponseType(typeof(List<WatchEntryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWatchEntries([FromQuery] int? genreId, [FromQuery] string? search)
    {
        var request = new GetWatchEntriesRequest(UserId) { GenreId = genreId, SearchString = search };
        var result = await mediator.Send(request);
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(WatchEntryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWatchEntry([FromRoute] int id)
    {
        var request = new GetWatchEntryRequest(UserId, id);
        var result = await mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateWatchEntry([FromBody] CreateWatchEntryDto watchEntry)
    {
        var command = new CreateWatchEntryCommand(UserId, watchEntry);
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetWatchEntry), new { id = result }, result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateWatchEntry([FromRoute] int id, [FromBody] UpdateWatchEntryDto watchEntry)
    {
        var command = new UpdateWatchEntryCommand(UserId, id, watchEntry);
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWatchEntry([FromRoute] int id)
    {
        var command = new DeleteWatchEntryCommand(UserId, id);
        await mediator.Send(command);
        return NoContent();
    }
}