namespace AdventOfCode2023.App;

using System.Reflection;
using AdventOfCode2023.App.Days;

public class Menu
{
    public static void StartScreen(string[]? args = null)
    {
        Console.WriteLine("Hello and Welcome to Advent of Code 2023!");
        Console.WriteLine("Please select a day to run:");
        string dayPathClassCheck = "";

        for (int i = 1; i <= 25; i++)
        {
            //check if the file exists
            dayPathClassCheck = @"App\Days\Day" + i + @"\Day" + i + ".cs";
            if (!File.Exists(GlobalSettings.GetBasePath() + dayPathClassCheck))
            {
                continue;
            }
            Console.WriteLine(i + ". Day " + i);
        }

        Console.WriteLine("0. Exit");
        Console.Write("Enter a number: ");
        string? input = Console.ReadLine();
        if (input == null)
        {
            Console.WriteLine("Invalid input\n\n\n");
            Menu.StartScreen(args);
            return;
        }
        int day = Convert.ToInt32(input);

        if (day == 0)
        {
            Console.WriteLine("Exiting...\n\n\n");
            return;
        }

        if (day < 1 || day > 25)
        {
            Console.WriteLine("Invalid input\n\n\n");
            Menu.StartScreen(args);
            return;
        }

        Functions.RunDayProblem(day);

        Menu.StartScreen(args);

    }

}

