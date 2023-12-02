using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Days
{
    public class Day2 : EveryDay, DayTest
    {
        public static string ProblemInput = "";

        public static string[] AllLines = [];

        public static List<SingleGameOfCubes> AllGames = new();

        public Day2()
        {
            ProblemInput = GlobalSettings.GetBasePath() + GetDayRootPath() + "InputProblem1.txt";

            if (!File.Exists(ProblemInput))
            {
                Console.WriteLine("Could not find input file for this day!");
                Console.WriteLine("Expected file at: " + ProblemInput);
                throw new Exception("Could not find input file for this day!");
            }
            AllLines = File.ReadAllLines(ProblemInput);

            if (AllLines == null || AllLines.Length == 0)
            {
                Console.WriteLine("Input file is empty!");
                throw new Exception("Input file is empty!");
            }

        }

        public override void Part1()
        {
            AllGames.Clear();
            //todays rules 12 red cubes, 13 green cubes, and 14 blue cubes.
            int TotalPossibleGamesAndItsIntScore = 0;

            foreach (string SingleLine in AllLines)
            {
                SingleGameOfCubes? NewGame = TranslateLineToGame(SingleLine);
                if (NewGame == null)
                {
                    Console.WriteLine("Could not translate line to game: " + SingleLine);
                    throw new Exception("Could not translate line to game: " + SingleLine);
                }
                AllGames.Add(NewGame);

                if (AllGames.Last().IsItPossible(12, 13, 14))
                {
                    Console.WriteLine("Game " + AllGames.Last().GameNumber + " is possible");
                    TotalPossibleGamesAndItsIntScore += AllGames.Last().GameNumber;
                }
            }

            Console.WriteLine("Total possible games sum of Int: " + TotalPossibleGamesAndItsIntScore);
            //Answer:  5050 too high    
            //Answer: 2#: 2716 Is correct

        }


        public override void Part2()
        {
        }

        private SingleGameOfCubes? TranslateLineToGame(string TextLine)
        {

            //"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"

            SingleGameOfCubes ReturnGame = new();

            //Game number
            TextLine = TextLine.Replace("Game ", "").Trim();
            string GameNumber = TextLine.Substring(0, TextLine.IndexOf(':'));
            ReturnGame.GameNumber = int.Parse(GameNumber);

            //Add all the sets of cubes
            TextLine = TextLine.Replace(GameNumber + ": ", "").Trim();
            string[] AllSets = TextLine.Split(';');

            foreach (string SingleSet in AllSets)
            {
                string[] AllCubes = SingleSet.Split(',');

                foreach (string SingleCube in AllCubes)
                {
                    string CubeColor = SingleCube.Trim().ToLower();

                    if (CubeColor.Contains("red"))
                    {
                        ReturnGame.RedCubes.Add(int.Parse(CubeColor.Replace("red", "").Trim()));
                    }
                    else if (CubeColor.Contains("green"))
                    {
                        ReturnGame.GreenCubes.Add(int.Parse(CubeColor.Replace("green", "").Trim()));
                    }
                    else if (CubeColor.Contains("blue"))
                    {
                        ReturnGame.BlueCubes.Add(int.Parse(CubeColor.Replace("blue", "").Trim()));
                    }
                    else
                    {
                        Console.WriteLine("Unknown cube color: " + CubeColor);
                        throw new Exception("Unknown cube color: " + CubeColor);
                    }
                }
            }

            Console.WriteLine("Game Number: " + ReturnGame.GameNumber + " has Sets: " + ReturnGame.GreenCubes.Count);

            return ReturnGame;

        }


        public class SingleGameOfCubes()
        {
            public int GameNumber = 0;

            public List<int> RedCubes = new();

            public List<int> GreenCubes = new();

            public List<int> BlueCubes = new();

            public bool IsItPossible(int MaxRedCubes = 0, int MaxGreenCubes = 0, int MaxBlueCubes = 0)
            {
                foreach (int SingleRedCube in RedCubes)
                {
                    if (SingleRedCube > MaxRedCubes)
                    {
                        return false;
                    }
                }

                foreach (int SingleGreenCube in GreenCubes)
                {
                    if (SingleGreenCube > MaxGreenCubes)
                    {
                        return false;
                    }
                }

                foreach (int SingleBlueCube in BlueCubes)
                {
                    if (SingleBlueCube > MaxBlueCubes)
                    {
                        return false;
                    }
                }

                return true;

            }

        }
    }


}