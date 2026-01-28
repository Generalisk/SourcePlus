global using static SourcePlus.Editor.Global;

using SourcePlus.Editor.Windows;

namespace SourcePlus.Editor;

internal static class Global
{
    public static List<Window> Windows { get; set; } = new List<Window>();
}
