namespace CinemaGoEF.Core.Exceptions;

public class AlreadyExistsException : CinemaGoException
{
    public AlreadyExistsException(string message)
        : base(message)
    {
    }
}