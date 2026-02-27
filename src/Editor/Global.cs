global using static SourcePlus.Editor.Global;

using SourcePlus.Editor.Windows;

namespace SourcePlus.Editor;

internal static class Global
{
    /// <summary>
    /// The local path of the active project
    /// </summary>
    public static string ProjectPath { get; internal set; } = "";



    internal static List<Window> Windows { get; set; } = new List<Window>();

    internal static bool WindowExists<T>() => WindowExists(typeof(T));

    internal static bool WindowExists(Type type)
    {
        foreach (var window in Windows)
            if (window.GetType() == type)
                return true;

        return false;
    }



    internal static Windows.Popups.Popup? Popup { get; set; } = null;
}
