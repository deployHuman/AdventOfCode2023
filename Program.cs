//Just run the Menu app main method
namespace AdventOfCode2023;

using AdventOfCode2023.App;

public static class Program
{
    public static void Main(string[] args)
    {
        Functions.RunDayProblem(null);
        Menu.StartScreen(args);
    }
}

