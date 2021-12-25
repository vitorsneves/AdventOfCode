namespace AdventOfCode
{
    internal class DayTwo
    {
        public static void PartOne()
        {
            List<string> commands = FileHandler.ReadFile("./DayTwoInput.txt");

            int forwardSteps = 0;
            int depth = 0;

            foreach(string command in commands)
            {
                string[] commandAndNumber = command.Split(" ");

                switch(commandAndNumber[0])
                {
                    case "forward":
                        forwardSteps += int.Parse(commandAndNumber[1]);
                        break;
                    case "up":
                        depth -= int.Parse(commandAndNumber[1]);
                        break;
                    case "down":
                        depth += int.Parse(commandAndNumber[1]);
                        break;
                }
            }

            int partOneSolution = forwardSteps * depth;

            System.Console.WriteLine("PART ONE:\n\tSteps forward -> " + forwardSteps + "\n\tTotal Depth -> " + depth);
            System.Console.WriteLine("\tSolution - >" + partOneSolution);
        }
        public static void PartTwo()
        {
            List<string> commands = FileHandler.ReadFile("./DayTwoInput.txt");

            int aim = 0,
                forward = 0,
                depth = 0;

            foreach(string command in commands)
            {
                string[] commandAndNumber = command.Split(" ");

                switch (commandAndNumber[0])
                {
                    case "forward":
                        forward += int.Parse(commandAndNumber[1]);
                        depth += aim * int.Parse(commandAndNumber[1]);
                        break;
                    case "up":
                        aim -= int.Parse(commandAndNumber[1]);
                        break;
                    case "down":
                        aim += int.Parse(commandAndNumber[1]);
                        break;
                }
            }

            int partTwoSolution = depth * forward;

            System.Console.WriteLine("PART TWO:\n\tSolution - >" + partTwoSolution);
        }
    }
}
