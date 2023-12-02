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
                Functions.DebugPrint("Could not find input file for this day!");
                Functions.DebugPrint("Expected file at: " + ProblemInput);
                throw new Exception("Could not find input file for this day!");
            }
            AllLines = File.ReadAllLines(ProblemInput);

            if (AllLines == null || AllLines.Length == 0)
            {
                Functions.DebugPrint("Input file is empty!");
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
                    Functions.DebugPrint("Could not translate line to game: " + SingleLine);
                    throw new Exception("Could not translate line to game: " + SingleLine);
                }
                AllGames.Add(NewGame);

                if (AllGames.Last().IsItPossible(12, 13, 14))
                {
                    Functions.DebugPrint("Game " + AllGames.Last().GameNumber + " is possible");
                    TotalPossibleGamesAndItsIntScore += AllGames.Last().GameNumber;
                }
            }

            Functions.PrintResult(TotalPossibleGamesAndItsIntScore.ToString(), "2", "1");
            //Answer:  5050 too high    
            //Answer: 2#: 2716 Is correct

        }


        public override void Part2()
        {
            AllGames.Clear();
            int SumOfthePowerOfLowestPossibleCubeScore = 0;

            foreach (string SingleLine in AllLines)
            {
                SingleGameOfCubes? NewGame = TranslateLineToGame(SingleLine);
                if (NewGame == null)
                {
                    throw new Exception("Could not translate line to game: " + SingleLine);
                }
                AllGames.Add(NewGame);
                SumOfthePowerOfLowestPossibleCubeScore += AllGames.Last().LowestPossibleCubeScore();

            }

            Functions.PrintResult(SumOfthePowerOfLowestPossibleCubeScore.ToString(), "2", "2");
            //Answer: 1#: 2735 too low
            //Answer: 2#:  72227 Is correct

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
                        continue;
                    }
                    if (CubeColor.Contains("green"))
                    {
                        ReturnGame.GreenCubes.Add(int.Parse(CubeColor.Replace("green", "").Trim()));
                        continue;

                    }
                    if (CubeColor.Contains("blue"))
                    {
                        ReturnGame.BlueCubes.Add(int.Parse(CubeColor.Replace("blue", "").Trim()));
                        continue;
                    }

                    Functions.DebugPrint("Unknown cube color: " + CubeColor);
                    throw new Exception("Unknown cube color: " + CubeColor);

                }
            }

            Functions.DebugPrint("Game Number: " + ReturnGame.GameNumber + " has Sets: " + ReturnGame.GreenCubes.Count);

            return ReturnGame;

        }


        public class SingleGameOfCubes()
        {
            public int GameNumber = 0;

            public List<int> RedCubes = new();

            public List<int> GreenCubes = new();

            public List<int> BlueCubes = new();


            public int LowestPossibleCubeScore()
            {
                int LowestRedCubes = 0;
                int LowestGreenCubes = 0;
                int LowestBlueCubes = 0;

                foreach (int SingleRedCube in RedCubes)
                {
                    if (SingleRedCube > LowestRedCubes)
                    {
                        LowestRedCubes = SingleRedCube;
                    }
                }

                foreach (int SingleGreenCube in GreenCubes)
                {
                    if (SingleGreenCube > LowestGreenCubes)
                    {
                        LowestGreenCubes = SingleGreenCube;
                    }
                }

                foreach (int SingleBlueCube in BlueCubes)
                {
                    if (SingleBlueCube > LowestBlueCubes)
                    {
                        LowestBlueCubes = SingleBlueCube;
                    }
                }

                return LowestRedCubes * LowestGreenCubes * LowestBlueCubes;

            }

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
