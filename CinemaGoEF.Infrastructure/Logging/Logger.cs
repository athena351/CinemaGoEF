using System.Net;
using System.Net.Sockets;
using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Infrastructure.FileStorage;

namespace CinemaGoEF.Infrastructure.Logging;

public class Logger : ILogger
{
    private readonly IFileManager _fileManager;

    public Logger(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }

    public async Task Log(string message)
    {
        string ip = GetUserIp();
        string logMessage = $"{DateTime.Now} - IP: {ip} - INFO - {message}";

        await _fileManager.AppendLine(DataPath.Logger, logMessage);
    }

    public async Task LogError(string message)
    {
        string ip = GetUserIp();
        string logMessage = $"{DateTime.Now} - IP: {ip} - ERROR - {message}";

        await _fileManager.AppendLine(DataPath.Logger, logMessage);
    }

    private string GetUserIp()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
        }
        catch (SocketException)
        {
        }

        return "Unknown";
    }
}