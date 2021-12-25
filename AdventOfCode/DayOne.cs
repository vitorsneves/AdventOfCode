namespace AdventOfCode
{
    internal class DayOne
    {
        private static List<int> ReadFile(string filePath)
        {
            return File.ReadAllLines(filePath).Select(x => int.Parse(x)).ToList();
        }
        public static void PartOne()
        {
            List<int> measurements = ReadFile("./DayOneInput.txt");
            int increasesInDepth = 0;

            for(int i = 1; i < measurements.Count; i++)
            {
                if(measurements[i] > measurements[i - 1])
                {
                    increasesInDepth++;
                }
            }

            System.Console.WriteLine("PART ONE: " + increasesInDepth);
        }

        public static void PartTwo()
        {
            List<int> measurements = ReadFile("./DayOneInput.txt");

            int commonSum = 0,
                currentGroupSum = 0,
                nextGroupSum = 0;

            int increasesInDepth = 0;

            for(int i = 0; i < measurements.Count - 3; i++)
            {
                commonSum = measurements[i + 1] + measurements[i + 2];
                currentGroupSum = commonSum + measurements[i];
                nextGroupSum = commonSum + measurements[i + 3];

                if(nextGroupSum > currentGroupSum)
                {
                    increasesInDepth++;
                }

                commonSum = 0;
                currentGroupSum = 0;
                nextGroupSum = 0;
            }

            System.Console.WriteLine("PART TWO: " + increasesInDepth);
        }
    }


}
