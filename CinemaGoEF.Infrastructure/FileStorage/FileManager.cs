using CinemaGoEF.Core.Interfaces;

namespace CinemaGoEF.Infrastructure.FileStorage;

public class FileManager : IFileManager
{
    public async Task<string[]> ReadAllLines(string path)
    {
        if (!File.Exists(path))
        {
            return Array.Empty<string>();
        }
        return await File.ReadAllLinesAsync(path);
    }

    public async Task WriteAllLines(string path, string[] lines)
    {
        await File.WriteAllLinesAsync(path, lines);
    }

    public async Task AppendLine(string path, string line)
    {
        await File.AppendAllTextAsync(path, line + Environment.NewLine);
    }
}