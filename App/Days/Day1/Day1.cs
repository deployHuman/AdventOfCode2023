using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Days
{
    public class Day1 : EveryDay, DayTest
    {
        public static string ProblemInput = "";

        public static string[] AllLines = [];

        public Day1()
        {
            ProblemInput = GlobalSettings.BasePath + GetDayRootPath() + "InputProblem1.txt";

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
            int SumReturn = 0;

            foreach (string line in AllLines)
            {
                Console.WriteLine(line);

                //use regex to replace all letters with nothing
                string resulst = Regex.Replace(line, "[^0-9]", "");
                // Console.WriteLine("Becomes " + resulst);

                int FirstNumber = Convert.ToInt32(resulst[0].ToString());
                int LastNumber = Convert.ToInt32(resulst[resulst.Length - 1].ToString());

                Console.WriteLine("Result: " + FirstNumber + " & " + LastNumber + " = " + Convert.ToInt16(FirstNumber.ToString() + LastNumber.ToString()));
                SumReturn += Convert.ToInt16(FirstNumber.ToString() + LastNumber.ToString());
            }

            Console.WriteLine("Total Sum: " + SumReturn);
            //1#: 6490 is too low
            //2#  35776 is too low
            //3#  54630  - Correct!
        }

        public override void Part2()
        {



        }

        private string ConvertAllNumbersToDigitsInString(string InputTextString)
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


            for (int i = 0; i < NumberDictionary.Count; i++)
            {
                if (InputTextString.Contains(NumberDictionary.ElementAt(i).Key))
                {
                    InputTextString = InputTextString.Replace(NumberDictionary.ElementAt(i).Key, NumberDictionary.ElementAt(i).Value.ToString());
                }
            }

            return InputTextString;

        }
    }

}
