namespace AdventOfCode
{
    static internal class DayThree
    {

        private static int[] getGamma (List<string> diagnosticReport)
        {

            int[] gammaRate = new int[diagnosticReport[0].Length];
            int[] numberOfOnes = DayThree.numberOfOnes(diagnosticReport);

            // Gets gamma rate.
            for (int i = 0; i < numberOfOnes.Length; i++)
            {
                if (numberOfOnes[i] >= Math.Ceiling((diagnosticReport.Count / 2.0)))
                {
                    gammaRate[i] = 1;
                }
                else
                {
                    gammaRate[i] = 0;
                }
            }

            return gammaRate;
        }

        // Count number of ones in each binary diagnostic column.
        private static int[] numberOfOnes(List<string> diagnosticReport)
        {
            int[] numberOfOnes = new int[diagnosticReport[0].Length];
            
            foreach (string line in diagnosticReport)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        numberOfOnes[i]++;
                    }
                }
            }

            return numberOfOnes;
        }

        // Change ones for zeroes and zeroes for ones.
        // Iterates over int array.
        private static int[] flipDigits(int[] number)
        {
            int[] flippedNumber = new int[number.Length];

            for (int i = 0; i < number.Length; i++)
            {
                if(number[i] == 1)
                {
                    flippedNumber[i] = 0;
                }
                else
                {
                    flippedNumber[i] = 1;
                }
            }

            return flippedNumber;
        }

        private static int BinaryArrayToDecimal(int[] binaryArray)
        {
            int decimalNumber = 0;

            for(int i = binaryArray.Length - 1; i >= 0; i--)
            {
                decimalNumber += (int)(binaryArray[i] * Math.Pow(2, (binaryArray.Length - 1) - i));
            }

            return decimalNumber;
        }
        private static int GetOxygenRating(List<string> reportRate)
        {
            // First "iteration".
            int[] majoritaryBit = DayThree.getGamma(reportRate);
            List<string> filteredReportRate = filterReportRate(reportRate, 0, majoritaryBit[0]);
            int iterationsCount = 1;

            while (filteredReportRate.Count > 1 && iterationsCount < reportRate[0].Length)
            {
                majoritaryBit = DayThree.getGamma(filteredReportRate);
                filteredReportRate = filterReportRate(filteredReportRate, iterationsCount, majoritaryBit[iterationsCount]);
                iterationsCount++;
            }

            return BinaryArrayToDecimal(stringToBinaryArray(filteredReportRate[0]));
        }

        private static int GetCO2ScrubberRate(List<string> reportRate)
        {
            // First "iteration".
            int[] minoritaryBit = flipDigits(DayThree.getGamma(reportRate));
            List<string> filteredReportRate = filterReportRate(reportRate, 0, minoritaryBit[0]);
            int iterationsCount = 1;

            while (filteredReportRate.Count > 1 && iterationsCount < reportRate[0].Length)
            {
                minoritaryBit = flipDigits(DayThree.getGamma(filteredReportRate));
                filteredReportRate = filterReportRate(filteredReportRate, iterationsCount, minoritaryBit[iterationsCount]);
                iterationsCount++;
            }

            return BinaryArrayToDecimal(stringToBinaryArray(filteredReportRate[0]));
        }

        private static void printList(List<string> list)
        {
            foreach(string line in list)
            {
                System.Console.WriteLine(line);
            }
        }

        private static int[] stringToBinaryArray(string stringNumber)
        {
            int[] binaryArray = new int[stringNumber.Length];

            for (int i = 0; i < stringNumber.Length; i++)
            {
                if(stringNumber[i] == '1')
                {
                    binaryArray[i] = 1;
                }
                else
                {
                    binaryArray[i] = 0;
                }
            }

            return binaryArray;
        }

        // Keeps lines with keyValue in the given position, removes the rest.
        private static List<string> filterReportRate(List<string> reportRate, int position, int keyValue)
        {
            List<string> filteredReportRate = new List<string>();

            foreach(string line in reportRate)
            {
                if (int.Parse(line[position].ToString()) == keyValue)
                {
                    filteredReportRate.Add(line);
                }
            }

            return filteredReportRate;
        }

        public static void PartOne()
        {
            List<string> reportRate = FileHandler.ReadFile("DayThreeInput.txt");

            int[] binaryGammaRate = getGamma(reportRate);
            int[] binaryEpsilonRate = flipDigits(binaryGammaRate);

            int gammaRate = BinaryArrayToDecimal(binaryGammaRate);
            int epsilonRate = BinaryArrayToDecimal(binaryEpsilonRate);
            int powerConsuption = gammaRate * epsilonRate;

            System.Console.WriteLine("PART ONE: Power Consuption -> " + powerConsuption);
        }

        public static void PartTwo()
        {
            List<string> reportRate = FileHandler.ReadFile("DayThreeInput.txt");

            int oxygenRate = GetOxygenRating(reportRate);
            int CO2ScrubberRate = GetCO2ScrubberRate(reportRate);

            System.Console.WriteLine("PART TWO: " + oxygenRate * CO2ScrubberRate);
        }
    }
}
