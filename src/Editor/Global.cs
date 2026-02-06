global using static SourcePlus.Editor.Global;

using SourcePlus.Editor.Windows;

namespace SourcePlus.Editor;

internal static class Global
{
    /// <summary>
    /// The local path of the active project
    /// </summary>
    public static string ProjectPath { get; set; } = "";

    public static List<Window> Windows { get; set; } = new List<Window>();

    public static bool WindowExists<T>() => WindowExists(typeof(T));

    public static bool WindowExists(Type type)
    {
        foreach (var window in Windows)
            if (window.GetType() == type)
                return true;

        return false;
    }
}
