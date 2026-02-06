using CineTrack.App.Features.Genres.GetGenres;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineTrack.Api.Controllers;

[ApiController]
[Route("api/cinetrack")]
public class CineTrackController(IMediator mediator) : ControllerBase
{
    [HttpGet("genres")]
    public async Task<IActionResult> GetGenres()
    {
        var command = new GetGenresCommand();
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    public async Task<IActionResult> GetMovies()
    {
        // Implement the logic to get movies here
        return Ok();
    }
}