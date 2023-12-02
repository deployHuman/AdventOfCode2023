
using System.Reflection;

public class GlobalSettings
{
    public static bool DebugMode = false;
    public static string GetBasePath()
    {
        //return where this file is located
        string path = Assembly.GetExecutingAssembly().Location;

        path = path.Substring(0, path.IndexOf("bin"));

        return path;
    }
}

