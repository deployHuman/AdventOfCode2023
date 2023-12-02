using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Days
{
    public class Day1 : EveryDay, DayTest
    {
        public static string ProblemInput = "";

        public static string[] AllLines = [];

        public Day1()
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
            Functions.PrintResult(CalculateResultNumber(AllLines), "1", "1");
            //1#: 6490 is too low
            //2#  35776 is too low
            //3#  54630  - Correct!
        }

        public override void Part2()
        {
            string[] SumString = new string[AllLines.Length];

            for (int i = 0; i < AllLines.Length; i++)
            {
                // Functions.DebugPrint("Line: " + AllLines[i] + " Becomes: " + ConvertAllNumbersToDigitsInString(AllLines[i]));
                SumString[i] = ConvertAllNumbersToDigitsInString(AllLines[i]);
            }

            Functions.PrintResult(CalculateResultNumber(SumString), "1", "2");
            //1# 54014 too low
            //2#  54520  too low
            //3#  54770  - Correct!
        }

        /// <summary>
        /// Calculate the sum of the first and last number in each line
        /// </summary>
        /// <param name="InputString"></param>
        /// <returns></returns>
        private string CalculateResultNumber(string[] InputString)
        {
            int SumReturn = 0;

            foreach (string line in InputString)
            {
                string resulst = Regex.Replace(line, "[^0-9]", "");

                string FirstNumber = resulst[0].ToString();
                string LastNumber = resulst[resulst.Length - 1].ToString();

                // Functions.DebugPrint("Result: " + FirstNumber + " & " + LastNumber + " = " + Convert.ToInt16(FirstNumber.ToString() + LastNumber.ToString()));
                SumReturn += Convert.ToInt16(FirstNumber.ToString() + LastNumber.ToString());
            }

            return SumReturn.ToString();

        }

        private static string ConvertAllNumbersToDigitsInString(string InputTextString)
        {
            Dictionary<string, int> NumberDictionary = new() {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };

            string ReturnString = "";

            for (int i = 0; i < InputTextString.Length; i++)
            {
                if (char.IsDigit(InputTextString[i]))
                {
                    ReturnString += InputTextString[i].ToString();
                    continue;
                }

                if (char.IsLetter(InputTextString[i]))
                {
                    for (int Y = 0; Y < NumberDictionary.Count; Y++)
                    {
                        if (i + NumberDictionary.ElementAt(Y).Key.Length > InputTextString.Length)
                        {
                            continue;
                        }

                        if (InputTextString.Substring(i, NumberDictionary.ElementAt(Y).Key.Length).ToLower() == NumberDictionary.ElementAt(Y).Key)
                        {
                            ReturnString += NumberDictionary.ElementAt(Y).Value.ToString();
                            i += 1;
                            continue;
                        }

                    }
                }
                ReturnString += InputTextString[i].ToString();
            }

            return ReturnString;

        }
    }

}
