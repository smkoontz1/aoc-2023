public static class FileReader
{
    public static string[] GetLines(string filename)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDirectory, filename);

        return File.ReadAllLines(filePath);
    }

}
