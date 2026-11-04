namespace CinemaGoEF.Core.Exceptions;

public class CinemaGoException : Exception
{
    public CinemaGoException(string message)
        : base(message)
    {
    }
}