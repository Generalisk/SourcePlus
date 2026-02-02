global using static SourcePlus.Debug;

namespace SourcePlus;

internal static class Debug
{
    internal static List<LogEntry> Logs { get; set; } = new List<LogEntry>();

    /// <summary>
    /// Writes a log message to the console; can be formatted
    /// </summary>
    /// <param name="text">The message to write to the console</param>
    /// <param name="args">Additional formatting arguments</param>
    public static void Log(string text, params object[] args)
    {
        Logs.Add(new LogEntry(LogType.Message, string.Format(text, args)));

        Console.WriteLine(text, args);
    }

    /// <summary>
    /// Writes a warning message to the console; can be formatted
    /// </summary>
    /// <param name="text">The message to write to the console</param>
    /// <param name="args">Additional formatting arguments</param>
    public static void LogWarning(string text, params object[] args)
    {
        Logs.Add(new LogEntry(LogType.Warning, string.Format(text, args)));

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
        Logs.Add(new LogEntry(LogType.Error, string.Format(text, args)));

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text, args);
        Console.ResetColor();
    }
}

internal struct LogEntry
{
    public LogType Type { get; }
    public string Message { get; }

    public LogEntry(LogType type, string message)
    {
        Type = type;
        Message = message;
    }
}

internal enum LogType
{
    Message = 0,
    Warning = 1,
    Error = 2,
}
