global using static SourcePlus.Editor.Global;

using SourcePlus.Editor.Windows;

namespace SourcePlus.Editor;

internal static class Global
{
    public static string ProjectPath { get; set; } = "";

    public static List<Window> Windows { get; set; } = new List<Window>();
}
