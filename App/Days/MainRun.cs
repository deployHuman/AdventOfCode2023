namespace AdventOfCode2023.App.Days
{
    public abstract class EveryDay
    {

        public string ProblemInput = "";

        public string[] AllLines = [];
        public void SolveProblems()
        {
            Part1();
            Part2();

        }
        public abstract void Part1();

        public abstract void Part2();

        public string GetDayRootPath()
        {
            return "App\\Days\\" + this.GetType().Name.ToString() + "\\";
        }

        public void ReadInProblem()
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
    }
}
