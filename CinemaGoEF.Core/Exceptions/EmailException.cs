namespace CinemaGoEF.Core.Exceptions;

public class EmailException : CinemaGoException
{
    public EmailException(string message)
        : base(message)
    {
    }
}