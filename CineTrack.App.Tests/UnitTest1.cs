using CineTrack.Api.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace CineTrack.App.Tests;

public class AuthenticationAttributesTests
{
    [Fact]
    public void GenresController_RequiresAuthorization()
    {
        var authorizeAttribute = typeof(GenresController).GetCustomAttributes(typeof(AuthorizeAttribute), true);
        Assert.NotEmpty(authorizeAttribute);
    }
}