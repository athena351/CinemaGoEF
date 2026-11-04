namespace CinemaGoEF.Core.Interfaces;

public interface IFileManager
{
    Task<string[]> ReadAllLines(string path);
    Task WriteAllLines(string path, string[] lines);
    Task AppendLine(string path, string line);
}