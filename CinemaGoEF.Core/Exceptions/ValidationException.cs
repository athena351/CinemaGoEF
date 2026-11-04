namespace CinemaGoEF.Core.Exceptions;

public class ValidationException : CinemaGoException
{
    public ValidationException(string message)
        : base(message)
    {
    }
}