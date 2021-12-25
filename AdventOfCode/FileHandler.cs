namespace AdventOfCode
{
    internal class FileHandler
    {
        public static List<string> ReadFile(string fileName)
        {
            return File.ReadAllLines(fileName).ToList();
        }
    }
}
