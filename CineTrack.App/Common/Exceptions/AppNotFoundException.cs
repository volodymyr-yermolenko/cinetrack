namespace CineTrack.App.Common.Exceptions;

public class AppNotFoundException(string message) : Exception(message)
{
}