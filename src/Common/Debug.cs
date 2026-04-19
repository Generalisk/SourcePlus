global using static SourcePlus.Debug;

using System;
using System.Collections.Generic;

namespace SourcePlus;

public static class Debug
{
    public static LogEntry[] Logs => logs.ToArray();
    private static List<LogEntry> logs = new List<LogEntry>();

    /// <summary>
    /// Writes a log message to the console; can be formatted
    /// </summary>
    /// <param name="text">The message to write to the console</param>
    /// <param name="args">Additional formatting arguments</param>
    public static void Log(string text, params object[] args)
    {
        var entry = new LogEntry(LogType.Message, string.Format(text, args));
        logs.Add(entry);

        Console.WriteLine(string.Format("[{1}] {0}", text, entry.Time.ToTimestamp()), args);
    }

    /// <summary>
    /// Writes a warning message to the console; can be formatted
    /// </summary>
    /// <param name="text">The message to write to the console</param>
    /// <param name="args">Additional formatting arguments</param>
    public static void LogWarning(string text, params object[] args)
    {
        var entry = new LogEntry(LogType.Warning, string.Format(text, args));
        logs.Add(entry);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(string.Format("[{1}] {0}", text, entry.Time.ToTimestamp()), args);
        Console.ResetColor();
    }

    /// <summary>
    /// Write a error message to the console; can be formatted
    /// </summary>
    /// <param name="text">The message to write to the console</param>
    /// <param name="args">Additional formatting arguments</param>
    public static void LogError(string text, params object[] args)
    {
        var entry = new LogEntry(LogType.Error, string.Format(text, args));
        logs.Add(entry);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(string.Format("[{1}] {0}", text, entry.Time.ToTimestamp()), args);
        Console.ResetColor();
    }

    /// <summary>
    /// Clear logs from console
    /// </summary>
    public static void Clear() => logs.Clear();

    public static string ToTimestamp(this TimeSpan time)
    {
        var hour = time.Hours.ToString();
        var minute = time.Minutes.ToString();
        var second = time.Seconds.ToString();

        hour = hour.Length > 1 ? hour : "0" + hour;
        minute = minute.Length > 1 ? minute : "0" + minute;
        second = second.Length > 1 ? second : "0" + second;

        return string.Format("{0}:{1}:{2}", hour, minute, second);
    }
}

public struct LogEntry
{
    public LogType Type { get; }
    public string Message { get; }
    public TimeSpan Time { get; } = DateTime.Now.TimeOfDay;

    public LogEntry(LogType type, string message)
    {
        Type = type;
        Message = message;
    }
}

public enum LogType
{
    Message = 0,
    Warning = 1,
    Error = 2,
}
