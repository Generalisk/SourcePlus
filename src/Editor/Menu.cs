using Raylib_cs;
using SourcePlus.Editor.Build;
using SourcePlus.Editor.Windows;
using SourcePlus.Editor.Windows.Popups;
using System.Threading;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor;

/// <summary>
/// The handler for the main menu bar
/// </summary>
internal static class Menu
{
    internal const int HEIGHT = 18;

    internal static void Draw()
    {
        if (BeginMainMenuBar())
        {
            if (BeginMenu("File"))
            {
                // TODO: Get output path from ask directory dialog
                if (MenuItem("Build"))
                    BuildSystem.Build("TestProjectOutput");

                EndMenu();
            }

            if (BeginMenu("Settings"))
            {
                if (BeginMenu("Framerate"))
                {
                    if (MenuItem("24 FPS"))
                        Raylib.SetTargetFPS(24);

                    if (MenuItem("30 FPS"))
                        Raylib.SetTargetFPS(30);

                    if (MenuItem("60 FPS"))
                        Raylib.SetTargetFPS(60);

                    if (MenuItem("120 FPS"))
                        Raylib.SetTargetFPS(120);

                    if (MenuItem("240 FPS"))
                        Raylib.SetTargetFPS(240);

                    if (MenuItem("300 FPS"))
                        Raylib.SetTargetFPS(300);

                    if (MenuItem("360 FPS"))
                        Raylib.SetTargetFPS(360);

                    if (MenuItem("420 FPS"))
                        Raylib.SetTargetFPS(420);

                    if (MenuItem("480 FPS"))
                        Raylib.SetTargetFPS(480);

                    if (MenuItem("600 FPS"))
                        Raylib.SetTargetFPS(600);

                    if (MenuItem("Unlimited"))
                        Raylib.SetTargetFPS(0);

                    EndMenu();
                }

                EndMenu();
            }

            if (BeginMenu("Window"))
            {
                foreach (var window in WindowHandler.GetNames())
                {
                    if (MenuItem(window))
                    {
                        var windowName = WindowHandler.GetType(window);
                        if (windowName != null) WindowHandler.Create(windowName);
                    }
                }

                EndMenu();
            }

#if DEBUG
            if (BeginMenu("Debug"))
            {
                if (MenuItem("Open AppData Folder"))
                    PathTools.OpenPath(AppDataPath);

                Separator();

                if (BeginMenu("Test Log"))
                {
                    if (MenuItem("Message"))
                        Log("Hello world!");

                    if (MenuItem("Warning"))
                        LogWarning("Hello world!");

                    if (MenuItem("Error"))
                        LogError("Hello world!");

                    EndMenu();
                }

                if (BeginMenu("Test Popup"))
                {
                    if (MenuItem("Generic"))
                        new GenericPopup("Test", "This is a test popup! I'm also writing additional text here just so I can make sure that the window adjusts to wrapped text correctly.");

                    if (MenuItem("Ask"))
                        new AskPopup("Test", "This is a test popup!", "Red pill", "Blue pill", (bool redPill) =>
                        { Log("Thy have tooken the {0} pill!", redPill ? "Red" : "Blue"); });

                    if (BeginMenu("Progress Bar"))
                    {
                        if (MenuItem("Standard"))
                            new Thread(() =>
                            {
                                for (float i = 0; i <= 1; i += 0.01f)
                                {
                                    ProgressBar.Draw("Test", "Lorem Ipsum", i);
                                    Thread.Sleep(69);
                                }

                                ProgressBar.Clear();
                            })
                            {
                                IsBackground = true,
                            }.Start();

                        if (MenuItem("Cancellable"))
                            new Thread(() =>
                            {
                                for (float i = 0; i <= 1; i += 0.01f)
                                {
                                    if (ProgressBar.DrawCancelable("Test", "Lorem Ipsum", i))
                                        break;
                                    Thread.Sleep(420);
                                }

                                ProgressBar.Clear();
                            })
                            {
                                IsBackground = true,
                            }.Start();

                        EndMenu();
                    }

                    EndMenu();
                }

                EndMenu();
            }
#endif // DEBUG

            if (BeginMenu("Help"))
            {
                if (MenuItem("Source code"))
                    PathTools.OpenURL("https://github.com/Generalisk/SourcePlus");

                if (MenuItem("Report issue"))
                    PathTools.OpenURL("https://github.com/Generalisk/SourcePlus/issues/new");

                EndMenu();
            }

            EndMainMenuBar();
        }
    }
}
