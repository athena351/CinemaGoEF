namespace CinemaGoEF.Core.Interfaces;

public interface IEmailService
{
    Task SendEmail(string sendTo, string subject, string body);
}