namespace CinemaGoEF.Infrastructure.FileStorage;

public static class DataPath
{
    private static string FindInfrastructurePath()
    {
        DirectoryInfo? directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory != null)
        {
            string path = Path.Combine(directory.FullName, "CinemaGo.Infrastructure"
            );

            if (Directory.Exists(path))
            {
                return path;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("CinemaGo.Infrastructure folder was not found."
        );
    }

    private static readonly string DataFolder = Path.Combine(FindInfrastructurePath(), "Data");

    static DataPath()
    {
        Directory.CreateDirectory(DataFolder);
    }

    public static string Logger => Path.Combine(DataFolder, "Logger.txt");
}