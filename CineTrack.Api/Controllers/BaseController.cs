using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CineTrack.Api.Controllers;

public class BaseController : ControllerBase
{
    protected int UserId => GetUserId();
    
    private int GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return !int.TryParse(userIdClaim, out var userId) 
            ? throw new UnauthorizedAccessException("User ID is not available for anonymous requests.") 
            : userId;
    }
}