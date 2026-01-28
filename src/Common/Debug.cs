global using static SourcePlus.Debug;

namespace SourcePlus;

internal static class Debug
{
    public static void Log(string text, params object[] args)
    {
        Console.WriteLine(text, args);
    }

    public static void LogWarning(string text, params object[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text, args);
        Console.ResetColor();
    }

    public static void LogError(string text, params object[] args)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text, args);
        Console.ResetColor();
    }
}
