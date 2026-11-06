using System.Net.Mail;
using CinemaGoEF.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CinemaGoEF.Application.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendEmail(string sendTo, string subject, string body)
    {
        var smtpServer = _configuration["EmailSettings:Server"];
        var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
        var smtpSender = _configuration["EmailSettings:Sender"];
        var smtpPassword = _configuration["EmailSettings:Password"];
        
        SmtpClient client = new SmtpClient(smtpServer, smtpPort);
        client.EnableSsl =  true;
        client.Credentials = new System.Net.NetworkCredential(smtpSender, smtpPassword);
        
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(smtpSender);
        mail.To.Add(sendTo);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;
        
        client.Send(mail);
    }
}