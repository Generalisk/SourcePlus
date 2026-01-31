global using static SourcePlus.Debug;

namespace SourcePlus;

internal static class Debug
{
    /// <summary>
    /// Writes a log message to the console; can be formatted
    /// </summary>
    /// <param name="text">The message to write to the console</param>
    /// <param name="args">Additional formatting arguments</param>
    public static void Log(string text, params object[] args)
    {
        Console.WriteLine(text, args);
    }

    /// <summary>
    /// Writes a warning message to the console; can be formatted
    /// </summary>
    /// <param name="text">The message to write to the console</param>
    /// <param name="args">Additional formatting arguments</param>
    public static void LogWarning(string text, params object[] args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text, args);
        Console.ResetColor();
    }

    /// <summary>
    /// Write a error message to the console; can be formatted
    /// </summary>
    /// <param name="text">The message to write to the console</param>
    /// <param name="args">Additional formatting arguments</param>
    public static void LogError(string text, params object[] args)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text, args);
        Console.ResetColor();
    }
}
