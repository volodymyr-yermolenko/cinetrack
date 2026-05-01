using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CineTrack.App.Features.Movies.CreateMovie;
using CineTrack.App.Features.Movies.DeleteMovie;
using CineTrack.App.Features.Movies.GetMovie;
using CineTrack.App.Features.Movies.GetMovies;
using CineTrack.App.Features.Movies.UpdateMovie;
using CineTrack.App.Models.Movies;

namespace CineTrack.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/movies")]
public class MoviesController(IMediator mediator) : BaseController
{
    [HttpGet("")]
    [ProducesResponseType(typeof(List<MovieDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovies([FromQuery] int? genreId, [FromQuery] string? search)
    {
        var request = new GetMoviesRequest(UserId) { GenreId = genreId, SearchString = search };
        var result = await mediator.Send(request);
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovie([FromRoute] int id)
    {
        var request = new GetMovieRequest(UserId, id);
        var result = await mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto movie) 
    {
        var command = new CreateMovieCommand(UserId, movie);
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetMovie), new { id = result }, result);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieDto movie) 
    {
        var command = new UpdateMovieCommand(UserId, id, movie);
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteMovie([FromRoute] int id) 
    {
        var command = new DeleteMovieCommand(UserId, id);
        await mediator.Send(command);
        return NoContent();
    }
}