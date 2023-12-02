using System.Reflection;

namespace AdventOfCode2023.App;

public class Functions
{

    public static void DebugPrint(string? Message)
    {
        if (GlobalSettings.DebugMode)
        {
            Console.WriteLine(Message);
        }
    }

    public static void PrintResult(string? Result, String DayNumber, string PartNumber)
    {
        Console.WriteLine("Day " + DayNumber + " Part " + PartNumber + " Result: " + Result);
    }

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
        Functions.DebugPrint("Running Day " + dayString);

        string dayPath = @"App\Days\Day" + dayString + @"\Day" + dayString + ".cs";

        //dynamically load the class using the name of the class

        if (!File.Exists(GlobalSettings.GetBasePath() + dayPath))
        {
            Functions.DebugPrint("Could not find Class file for that day");
            Functions.DebugPrint("Please make sure the file exists at: " + GlobalSettings.GetBasePath() + dayPath + "\n\n\n");
            Menu.StartScreen(null);
            return;
        }


        object? DayObject = Functions.CreateNewDynamicDayObject(dayString);

        if (DayObject == null)
        {
            Functions.DebugPrint("Invalid input\n\n\n");
            return;
        }

        MethodInfo? runTestsMethod = DayObject.GetType().GetMethod("SolveProblems");

        if (runTestsMethod == null)
        {
            Functions.DebugPrint("Could not find That days SolveProblems method\n\n\n");
            return;
        }

        runTestsMethod.Invoke(DayObject, null);

        Functions.DebugPrint("Problem Solved.");
        Functions.DebugPrint("#######################################################\n\n\n");
    }

    public static object? CreateNewDynamicDayObject(string DayString)
    {
        string dayPath = @"App\Days\Day" + DayString + @"\Day" + DayString + ".cs";

        string ClassPath = GlobalSettings.GetBasePath() + dayPath;

        if (!File.Exists(ClassPath))
        {
            Functions.DebugPrint("Could not find Class file for that day");
            Functions.DebugPrint("Please make sure the file exists at: " + ClassPath);
            Menu.StartScreen(null);
            return null;
        }

        //AdventOfCode2023.App.Days
        Type? type = Type.GetType("AdventOfCode2023.App.Days.Day" + DayString);
        if (type == null)
        {
            Functions.DebugPrint("Typpe Not found");
            Menu.StartScreen(null);
            return null;
        }

        object? instance = Activator.CreateInstance(type);
        if (instance == null)
        {
            Functions.DebugPrint("Instance Not found");
            Menu.StartScreen(null);
            return null;
        }

        return instance;
    }
}