using SourcePlus.Editor.Windows;
using SourcePlus.Editor.Windows.Popups;
using System.Reflection;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor;

/// <summary>
/// The handler for the main menu bar
/// </summary>
internal static class Menu
{
    internal static void Draw()
    {
        if (BeginMainMenuBar())
        {
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

                    EndMenu();
                }

                EndMenu();
            }
#endif // DEBUG

            EndMainMenuBar();
        }
    }
}
