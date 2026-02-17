using CineTrack.App.Features.Movies.CreateMovie;
using CineTrack.App.Features.Movies.DeleteMovie;
using CineTrack.App.Features.Movies.GetMovie;
using CineTrack.App.Features.Movies.GetMovies;
using CineTrack.App.Features.Movies.UpdateMovie;
using CineTrack.App.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineTrack.Api.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController(IMediator mediator) : ControllerBase
{
    private const int DevUserId = 1;
    
    [HttpGet("")]
    [ProducesResponseType(typeof(List<MovieDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovies([FromQuery] int? genreId)
    {
        var request = new GetMoviesRequest(DevUserId) { GenreId = genreId };
        var result = await mediator.Send(request);
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovie(int id)
    {
        var request = new GetMovieRequest(DevUserId) { MovieId = id };
        var result = await mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto movie) 
    {
        var command = new CreateMovieCommand(DevUserId) { Movie = movie };
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetMovie), new { id = result }, result);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieDto movie) 
    {
        var command = new UpdateMovieCommand(DevUserId) { MovieId = id, Movie = movie };
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteMovie(int id) 
    {
        var command = new DeleteMovieCommand(DevUserId) { MovieId = id };
        await mediator.Send(command);
        return NoContent();
    }
}