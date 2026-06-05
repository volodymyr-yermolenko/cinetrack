using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using CineTrack.App.Features.Genres.GetGenres;
using CineTrack.App.Models.Genres;

namespace CineTrack.Api.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController(IMediator mediator): BaseController
{
    [HttpGet("")]
    [ProducesResponseType(typeof(List<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenres()
    {
        var request = new GetGenresRequest();
        var result = await mediator.Send(request);
        return Ok(result);
    }
}