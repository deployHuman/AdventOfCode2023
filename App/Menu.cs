namespace AdventOfCode2023.App;

using System.Reflection;
using AdventOfCode2023.App.Days;

public class Menu
{
    public static void StartScreen(string[]? args = null)
    {
        // Console.Clear();
        Console.WriteLine("Hello and Welcome to Advent of Code 2023!");
        Console.WriteLine("Please select a day to run:");
        string dayPathClassCheck = "";

        for (int i = 1; i <= 25; i++)
        {
            //check if the file exists
            dayPathClassCheck = @"App\Days\Day" + i + @"\Day" + i + ".cs";
            if (!File.Exists(GlobalSettings.BasePath + dayPathClassCheck))
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

        Console.WriteLine("Running Day " + day);

        string dayString = day.ToString();

        string dayPath = @"App\Days\Day" + dayString + @"\Day" + dayString + ".cs";

        //dynamically load the class using the name of the class

        if (!File.Exists(GlobalSettings.BasePath + dayPath))
        {
            Console.WriteLine("Could not find Class file for that day");
            Console.WriteLine("Please make sure the file exists at: " + GlobalSettings.BasePath + dayPath + "\n\n\n");
            Menu.StartScreen(args);
            return;
        }


        object? DayObject = Functions.CreateNewDynamicDayObject(dayString);

        if (DayObject == null)
        {
            Console.WriteLine("Invalid input\n\n\n");
            Menu.StartScreen(args);
            return;
        }

        MethodInfo? runTestsMethod = DayObject.GetType().GetMethod("SolveProblems");

        if (runTestsMethod == null)
        {
            Console.WriteLine("Could not find That days SolveProblems method\n\n\n");
            Menu.StartScreen(args);
            return;
        }

        runTestsMethod.Invoke(DayObject, null);

        Console.WriteLine("Tests completed, Returning to main menu");
        Console.WriteLine("#######################################################\n\n\n");
        Menu.StartScreen(args);

    }

}

