using System;

namespace AdventOfCode;
internal static class DayFive
{
    private static List<List<int>> ReadFile(string filePath)
    {
        var fileMap = new List<List<int>>();
        var numberSeparators = new string[] { " -> ", "," };

        return File.ReadAllLines(filePath).Select(line =>
            line.Split(numberSeparators, StringSplitOptions.RemoveEmptyEntries)
            .Select(number => int.Parse(number))
            .ToList()
        ).ToList();
    }

    private static void AddLineToMap(List<List<int>> map, List<int> line)
    {
        var x = line[0];
        var y = line[1];

        while (x != line[2] || y != line[3])
        {
            map[y][x]++;

            if (x < line[2]) x++;
            if (x > line[2]) x--;
            if (y < line[3]) y++;
            if (y > line[3]) y--;
        }

        map[y][x]++;
    }

    private static List<List<int>> CreateMap(int rows, int columns)
    {
        var map = new List<List<int>>();

        for (var i = 0; i < rows; i++)
        {
            map.Add(new List<int>());

            for (var j = 0; j < columns; j++)
                map[i].Add(0);
        }

        return map;
    }

    private static bool IsLineDiagonal(List<int> line)
    {
        return line[0] != line[2] && line[1] != line[3];
    }

    private static int CountIntersections(List<List<int>> map)
    {
        int count = 0;

        foreach (var line in map)
            foreach (var pixel in line)
                if (pixel >= 2) count++;

        return count;
    }

    public static void PartOne()
    {
        var lines = ReadFile("./DayFiveInput.txt");
        var map = CreateMap(1000, 1000);

        foreach (var line in lines)
        {
            if (IsLineDiagonal(line)) continue;

            AddLineToMap(map, line);
        }

        int intersectionsCount = CountIntersections(map);
        Console.WriteLine("The lines overlap at " + intersectionsCount + " different spots");
    }

    public static void PartTwo()
    {
        var lines = ReadFile("./DayFiveInput.txt");
        var map = CreateMap(1000, 1000);

        foreach (var line in lines) 
            AddLineToMap(map, line);

        int intersectionsCount = CountIntersections(map);
        Console.WriteLine("The lines overlap at " + intersectionsCount + " different spots");
    }
}
