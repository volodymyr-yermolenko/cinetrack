using CineTrack.Api.Constants;
using CineTrack.App.Features.Genres.GetGenres;
using CineTrack.App.Features.Movies.GetMovie;
using CineTrack.App.Features.Movies.GetMovies;
using CineTrack.App.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineTrack.Api.Controllers;

[ApiController]
[Route("api/cinetrack")]
public class CineTrackController(IMediator mediator) : ControllerBase
{
    private const int DevUserId = 1;
    
    [HttpGet("genres")]
    [Produces(ApiConstants.ContentJson, Type = typeof(List<GenreDto>))]
    public async Task<IActionResult> GetGenres()
    {
        var command = new GetGenresRequest();
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet("movies")]
    [Produces(ApiConstants.ContentJson, Type = typeof(List<MovieDto>))]
    public async Task<IActionResult> GetMovies()
    {
        var command = new GetMoviesRequest(DevUserId);
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet("movies/{id:int}")]
    [Produces(ApiConstants.ContentJson, Type = typeof(List<MovieDto>))]
    public async Task<IActionResult> GetMovie(int id)
    {
        var command = new GetMovieRequest(DevUserId) { MovieId = id };
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
}