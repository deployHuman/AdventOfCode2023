using System.Reflection;

namespace AdventOfCode2023.App;

public class Functions
{

    public static void RunDayProblem(int? WhatDayToRun)
    {
        string? dayString;

        if (WhatDayToRun == null || WhatDayToRun == 0)
        {
            dayString = System.DateTime.Today.Day.ToString();
        }
        else
        {
            dayString = WhatDayToRun.ToString();
        }
        Console.WriteLine("Running Day " + dayString);

        string dayPath = @"App\Days\Day" + dayString + @"\Day" + dayString + ".cs";

        //dynamically load the class using the name of the class

        if (!File.Exists(GlobalSettings.GetBasePath() + dayPath))
        {
            Console.WriteLine("Could not find Class file for that day");
            Console.WriteLine("Please make sure the file exists at: " + GlobalSettings.GetBasePath() + dayPath + "\n\n\n");
            Menu.StartScreen(null);
            return;
        }


        object? DayObject = Functions.CreateNewDynamicDayObject(dayString);

        if (DayObject == null)
        {
            Console.WriteLine("Invalid input\n\n\n");
            return;
        }

        MethodInfo? runTestsMethod = DayObject.GetType().GetMethod("SolveProblems");

        if (runTestsMethod == null)
        {
            Console.WriteLine("Could not find That days SolveProblems method\n\n\n");
            return;
        }

        runTestsMethod.Invoke(DayObject, null);

        Console.WriteLine("Problem Solved.");
        Console.WriteLine("#######################################################\n\n\n");
    }

    public static object? CreateNewDynamicDayObject(string DayString)
    {
        string dayPath = @"App\Days\Day" + DayString + @"\Day" + DayString + ".cs";

        string ClassPath = GlobalSettings.GetBasePath() + dayPath;

        if (!File.Exists(ClassPath))
        {
            Console.WriteLine("Could not find Class file for that day");
            Console.WriteLine("Please make sure the file exists at: " + ClassPath);
            Menu.StartScreen(null);
            return null;
        }

        //AdventOfCode2023.App.Days
        Type? type = Type.GetType("AdventOfCode2023.App.Days.Day" + DayString);
        if (type == null)
        {
            Console.WriteLine("Typpe Not found");
            Menu.StartScreen(null);
            return null;
        }

        object? instance = Activator.CreateInstance(type);
        if (instance == null)
        {
            Console.WriteLine("Instance Not found");
            Menu.StartScreen(null);
            return null;
        }

        return instance;
    }
}