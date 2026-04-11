global using static SourcePlus.Editor.Global;
global using static SourcePlus.Debug;

using Raylib_cs;
using SourcePlus.Editor.Windows;
using SourcePlus.Editor.Windows.Popups;
using System;
using System.Collections.Generic;

namespace SourcePlus.Editor;

internal static class Global
{
    /// <summary>
    /// The local path of the active project
    /// </summary>
    public static string ProjectPath { get; internal set; } = "";

    /// <summary>
    /// The local path of the users (roaming) app data folder
    /// </summary>
    public static string AppDataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SourcePlus";



    internal static List<Window> ActiveWindows { get; set; } = new List<Window>();



    internal static Popup? ActivePopup { get; set; } = null;



    /// <summary>
    /// Refreshes the application title
    /// </summary>
    internal static void UpdateTitle()
    {
        var name = ProjectInfo.Instance.Name;
        var openglVersion = Rlgl.GetVersion();

        string opengl;
        switch (openglVersion)
        {
            case GlVersion.OpenGl11:
                opengl = "OpenGL 1.1";
                break;
            case GlVersion.OpenGl21:
                opengl = "OpenGL 2.1";
                break;
            case GlVersion.OpenGl33:
                opengl = "OpenGL 3.3";
                break;
            case GlVersion.OpenGl43:
                opengl = "OpenGL 4.3";
                break;
            case GlVersion.OpenGlEs20:
                opengl = "OpenGL ES 2";
                break;
            default:
                opengl = "OpenGL";
                break;
        }

        Raylib.SetWindowTitle(string.Format("{0} - Source+ [{1}]", name, opengl));
    }
}
