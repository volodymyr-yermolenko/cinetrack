using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CineTrack.Infrastructure.Common.Helpers;

public static class SecurityKeyHelper
{
    public static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    }
}