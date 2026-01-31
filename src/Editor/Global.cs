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
}
