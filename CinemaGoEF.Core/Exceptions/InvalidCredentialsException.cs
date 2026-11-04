namespace CinemaGoEF.Core.Exceptions;

public class InvalidCredentialsException : CinemaGoException
{
    public InvalidCredentialsException(string message)
        : base(message)
    {
    }
}