using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Days
{
    public class Day3 : EveryDay, DayTest
    {
        public Dictionary<int, Dictionary<int, string>> TheWholeMap = new();

        public List<NumberGroup> AllNumberGroups = new();

        public Day3()
        {
            ReadInProblem();
            ReadInAsMap();
            SetAllNumbersFromLines();
        }

        private void ReadInAsMap()
        {
            int x = 0;
            int y = 0;
            foreach (string line in AllLines)
            {
                x = 0;
                foreach (char c in line)
                {
                    if (!TheWholeMap.ContainsKey(x))
                    {
                        TheWholeMap.Add(x, new Dictionary<int, string>());
                    }
                    if (!TheWholeMap[x].ContainsKey(y))
                    {
                        TheWholeMap[x].Add(y, "");
                    }
                    TheWholeMap[x][y] = c.ToString();
                    x++;
                }
                y++;
            }
        }

        private void SetAllNumbersFromLines()
        {
            AllNumberGroups.Clear();

            int yCord = 0;
            foreach (string line in AllLines)
            {
                foreach (Match match in Regex.Matches(line, @"\d+"))
                {
                    NumberGroup numberGroup = new()
                    {
                        Number = int.Parse(match.Value),
                        StartingCoordinate = new Coordinate { X = match.Index, Y = yCord }
                    };
                    numberGroup.CalculateAdjecent();

                    Functions.DebugPrint("Number: " + numberGroup.Number + " at " + numberGroup.StartingCoordinate.X + "," + numberGroup.StartingCoordinate.Y);

                    AllNumberGroups.Add(numberGroup);
                }
                yCord++;
            }
        }


        public override void Part1()
        {
            int TotalSumOfParts = 0;
            foreach (NumberGroup numberGroup in AllNumberGroups)
            {
                string Content = GetContentFromAllAdjecentCoords(numberGroup);
                bool IsPart = DoesItContainAPart(Content);
                Functions.DebugPrint("Number " + numberGroup.Number + " at " + numberGroup.StartingCoordinate.X + "," + numberGroup.StartingCoordinate.Y + " has content: " + Content + " Is there a Part here? : " + IsPart.ToString());
                Functions.DebugPrint("###\n");
                if (IsPart)
                {
                    TotalSumOfParts += numberGroup.Number;
                }
            }
            Functions.PrintResult(TotalSumOfParts.ToString(), 3, 1);
            //409423 is too low
        }

        private bool DoesItContainAPart(string AdjacenContent)
        {
            AdjacenContent = Regex.Replace(AdjacenContent, @"\d+|\.+| +", "").Trim();
            Functions.DebugPrint("Filtered AdjacenContent : '" + AdjacenContent + "' Length: " + AdjacenContent.Length);
            return !(AdjacenContent.Length == 0);
        }

        public string GetContentFromAllAdjecentCoords(NumberGroup numberGroup)
        {
            string content = "";
            foreach (Coordinate coord in numberGroup.AdjacentCoords)
            {
                content += GetContentFromCoord(coord);
            }
            return content;
        }

        public string? GetContentFromCoord(Coordinate coord)
        {
            if (TheWholeMap.ContainsKey(coord.X))
            {
                if (TheWholeMap[coord.X].ContainsKey(coord.Y))
                {
                    return TheWholeMap[coord.X][coord.Y];
                }
            }
            return null;
        }

        public class NumberGroup
        {
            public int Number;

            public Coordinate StartingCoordinate = new();

            public List<Coordinate> AdjacentCoords = [];

            public void CalculateAdjecent()
            {
                //before the number on the left
                AdjacentCoords.Add(new Coordinate { X = StartingCoordinate.X - 1, Y = StartingCoordinate.Y });

                //before the number on the left, one up
                AdjacentCoords.Add(new Coordinate { X = StartingCoordinate.X - 1, Y = StartingCoordinate.Y - 1 });

                //before the number on the left, one down
                AdjacentCoords.Add(new Coordinate { X = StartingCoordinate.X - 1, Y = StartingCoordinate.Y + 1 });

                //after the number on the right
                AdjacentCoords.Add(new Coordinate { X = StartingCoordinate.X + Number.ToString().Length + 1, Y = StartingCoordinate.Y });

                //after the number on the right, one up
                AdjacentCoords.Add(new Coordinate { X = StartingCoordinate.X + Number.ToString().Length + 1, Y = StartingCoordinate.Y - 1 });

                //after the number on the right, one down
                AdjacentCoords.Add(new Coordinate { X = StartingCoordinate.X + Number.ToString().Length + 1, Y = StartingCoordinate.Y + 1 });

                //above the number
                for (int i = 0; i < Number.ToString().Length; i++)
                {
                    AdjacentCoords.Add(new Coordinate { X = StartingCoordinate.X + i, Y = StartingCoordinate.Y - 1 });
                }

                //below the number
                for (int i = 0; i < Number.ToString().Length; i++)
                {
                    AdjacentCoords.Add(new Coordinate { X = StartingCoordinate.X + i, Y = StartingCoordinate.Y + 1 });
                }
            }
        }

        /// <summary>
        /// A coordinate on the grid
        /// X is horizontal, Y is vertical
        /// All cooridnates being from the top left and go down and right
        /// </summary>
        public class Coordinate
        {
            public int X;
            public int Y;
            public string Content = "";
        }


        public override void Part2()
        {


        }
    }


}
