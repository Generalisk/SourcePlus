global using static SourcePlus.Editor.Global;

using SourcePlus.Editor.Windows;
using SourcePlus.Editor.Windows.Popups;

namespace SourcePlus.Editor;

internal static class Global
{
    /// <summary>
    /// The local path of the active project
    /// </summary>
    public static string ProjectPath { get; internal set; } = "";



    internal static List<Window> ActiveWindows { get; set; } = new List<Window>();



    internal static Popup? ActivePopup { get; set; } = null;
}
