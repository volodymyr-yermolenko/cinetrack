using System.Text.RegularExpressions;
using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;

namespace CineTrack.App.Common.Helpers;

public static class ValidationHelper
{
    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new AppValidationException(AuthErrorMessages.EmailRequired);
        }
        
        const string pattern = @"^(?!.*\.\.)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        if (!Regex.IsMatch(email, pattern))
        {
            throw new AppValidationException(AuthErrorMessages.InvalidEmail);
        }
    }

    public static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new AppValidationException(AuthErrorMessages.PasswordRequired);
        }
        if (password != password.Trim())
        {
            throw new AppValidationException(AuthErrorMessages.PasswordContainsOuterSpaces);
        }
        
        const string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
        if (!Regex.IsMatch(password, pattern))
        {
            throw new AppValidationException(AuthErrorMessages.InvalidPassword);
        }
    }
}