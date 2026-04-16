using System.Text.RegularExpressions;
using CineTrack.App.Common.Exceptions;

namespace CineTrack.App.Common.Helpers;

public static class ValidationHelper
{
    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new AppValidationException("Email is required");
        }
        
        const string pattern = @"^(?!.*\.\.)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        if (!Regex.IsMatch(email, pattern))
        {
            throw new AppValidationException("Email address is not valid");
        }
    }

    public static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new AppValidationException("Password is required");
        }
        if (password != password.Trim())
        {
            throw new AppValidationException("Password must not start or end with spaces");
        }
        
        const string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
        if (!Regex.IsMatch(password, pattern))
        {
            throw new AppValidationException("Password must be at least 8 characters long and contain uppercase, lowercase letters, and numbers");
        }
    }
}