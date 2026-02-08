namespace CineTrack.App.Exceptions;

public class AppNotFoundException(string message) : Exception(message)
{
}