namespace CinemaGoEF.Core.Interfaces;

public interface ILogger
{
    Task Log(string message);
    Task LogError(string message);
}