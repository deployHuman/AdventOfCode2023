namespace AdventOfCode2023.App.Days
{
    public abstract class EveryDay
    {
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
    }
}
