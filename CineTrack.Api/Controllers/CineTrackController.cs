using CineTrack.App.Features.Genres.GetGenres;
using CineTrack.App.Features.Movies.GetMovies;
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
        var command = new GetGenresRequest();
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet("movies/{userId:int}")]
    public async Task<IActionResult> GetMovies(int userId)
    {
        var command = new GetMoviesRequest(userId);
        var result = await mediator.Send(command);
        return Ok(result);
    }
}