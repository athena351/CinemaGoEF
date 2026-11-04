namespace CinemaGoEF.Core.Exceptions;

public class NotFoundException : CinemaGoException
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}