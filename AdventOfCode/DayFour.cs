namespace AdventOfCode
{
    public static class DayFour
    {
        private static List<Board> ReadBoards(string filePath)
        {
            List<string> fileData = File.ReadAllLines(filePath).ToList();
            fileData = fileData.Skip(2).ToList();

            List<Board> allBoards = new List<Board>();
            List<List<int>> currentBoardNums = new List<List<int>>();

            bool boardEndReached = false;
            bool FistTimeRunning = true;

            foreach (var line in fileData)
            {
                if(boardEndReached && !FistTimeRunning) 
                {
                    allBoards.Add(new Board(currentBoardNums));
                    currentBoardNums = new List<List<int>>();
                    boardEndReached = false;
                }

                if (line == "")
                {
                    boardEndReached = true;
                    continue;
                }

                List<int> lineNumbers = line.Trim(' ').Replace("  ", " ").Split(" ").Select((x) => int.Parse(x)).ToList();

                FistTimeRunning = false;
                currentBoardNums.Add(lineNumbers);
            }

            allBoards.Add(new Board(currentBoardNums));

            return allBoards;
        }

        private static List<int> ReadChoosedNumbers(string filePath)
        {
            List<string> fileData = File.ReadAllLines(filePath).ToList();
            List<int> choosedNumbers = fileData[0].Split(",").Select(x => int.Parse(x)).ToList();

            return choosedNumbers;
        }

        public static void PartOne()
        {
            List<int> choosedNumbers = ReadChoosedNumbers("DayFourInput.txt");
            List<Board> allBoards = ReadBoards("DayFourInput.txt");

            Board? winner = null;
            int count = 0;

            while(Object.ReferenceEquals(winner, null) && count < choosedNumbers.Count)
            {
                foreach (var board in allBoards)
                {
                    board.SearchNumber(choosedNumbers[count]);
                    if(board.IsBoardWinner())
                    {
                        winner = board;
                    }
                }
                count++;
            }

            System.Console.WriteLine("PART ONE: Winner board score: " + winner.GetScore(choosedNumbers[count - 1]));
        }

        public static void PartTwo()
        {
            List<int> choosedNumbers = ReadChoosedNumbers("DayFourInput.txt");
            List<Board> allBoards = ReadBoards("DayFourInput.txt");

            int currentNumber = 0;

            // Remove winners.
            while(allBoards.Count > 1)
            {
                foreach(var board in allBoards)
                {
                    board.SearchNumber(choosedNumbers[currentNumber]);
                    if(board.IsBoardWinner())
                    {
                        allBoards = allBoards.Where(o => !o.Equals(board)).ToList();
                    }
                }
                currentNumber++;
            }

            // Continue game until last board wins.
            while (!allBoards[0].IsBoardWinner())
            {
                allBoards[0].SearchNumber(choosedNumbers[currentNumber]);
                currentNumber++;
            }

            System.Console.WriteLine("PART TWO: Loser board score: " + allBoards[0].GetScore(choosedNumbers[currentNumber - 1]));
        }
    }



    public class Board
    {
        private List<List<int>> boardNumbers;
        private List<List<bool>> markedCells = new List<List<bool>>();

        public Board(List<List<int>> boardNumbers)
        {
            this.boardNumbers = boardNumbers;
            for(int i = 0; i < boardNumbers.Count; i++)
            {
                markedCells.Add(new List<bool>());
                for(int j = 0; j < boardNumbers[i].Count; j++)
                {
                    markedCells[i].Add(false);
                }
            }
        }

        public override string ToString()
        {
            string result = "";

            for(int i = 0; i < boardNumbers.Count; i++)
            {
                for(int j = 0; j < boardNumbers[0].Count; j++)
                {
                    if(markedCells[i][j] == true)
                    {
                        result += "x";
                    }
                    result += boardNumbers[i][j] + " ";
                }

                result += "\n";
            }

            return result;
        }

        public int GetScore(int lastNum)
        {
            int score = 0;

            for(int i = 0; i < boardNumbers.Count; i++)
            {
                for(int j = 0; j < boardNumbers[i].Count; j++)
                {
                    if (!markedCells[i][j])
                    {
                        score += boardNumbers[i][j];
                    }
                }
            }

            return score * lastNum;
        }

        // Searches and marks the board.
        public bool SearchNumber(int number)
        {
            for(int i = 0; i < boardNumbers.Count; i++)
            {
                for(int j = 0; j < boardNumbers[0].Count; j++)
                {
                    if(boardNumbers[i][j] == number)
                    {
                        markedCells[i][j] = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsBoardWinner()
        {
            for(int i = 0; i < boardNumbers.Count; i++)
            {
                if(CheckBoardLine(i))
                {
                    return true;
                }
                if(CheckBoardColumn(i)) {
                    return true;
                }
            }
            return false;
        }

        // Checks if the given column's values are all marked.
        private bool CheckBoardColumn(int columnNum)
        {
            for (int i = 0; i < boardNumbers[0].Count; i++)
            {
                if (!markedCells[i][columnNum])
                {
                    return false;
                }
            }

            return true;
        }

        // Checks if the given line's values are all marked.
        private bool CheckBoardLine(int lineNum)
        {
            for (int i = 0; i < boardNumbers.Count; i++)
            {
                if(!markedCells[lineNum][i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
