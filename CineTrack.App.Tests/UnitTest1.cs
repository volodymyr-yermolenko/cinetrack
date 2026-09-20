using CineTrack.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace CineTrack.App.Tests;

public class AuthenticationAttributesTests
{
    [Fact]
    public void GenresController_RequiresAuthorization()
    {
        var authorizeAttribute = typeof(GenresController).GetCustomAttributes(typeof(AuthorizeAttribute), true);
        Assert.NotEmpty(authorizeAttribute);

        var controllerAllowAnonymous = typeof(GenresController).GetCustomAttributes(typeof(AllowAnonymousAttribute), true);
        Assert.Empty(controllerAllowAnonymous);

        var actionAllowAnonymous = typeof(GenresController)
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .SelectMany(method => method.GetCustomAttributes(typeof(AllowAnonymousAttribute), true));
        Assert.Empty(actionAllowAnonymous);
    }
}