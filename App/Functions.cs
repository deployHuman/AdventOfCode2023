namespace AdventOfCode2023.App;

public class Functions
{



    public static object? CreateNewDynamicDayObject(string DayString)
    {
        string dayPath = @"App\Days\Day" + DayString + @"\Day" + DayString + ".cs";

        string ClassPath = GlobalSettings.BasePath + dayPath;

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