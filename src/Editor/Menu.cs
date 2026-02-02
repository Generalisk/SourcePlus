using SourcePlus.Editor.Windows;
using System.Reflection;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor;

/// <summary>
/// The handler for the main menu bar
/// </summary>
internal static class Menu
{
    private static KeyValuePair<string, Type>[] windows = { };

    internal static void Init()
    {
        // Retrieve window menu buttons
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Window)));

        var windows = new List<KeyValuePair<string, Type>>();

        foreach (var type in types)
        {
            var window = (Window)Activator.CreateInstance(type);
            windows.Add(new KeyValuePair<string, Type>(window.Name, type));
            window.Dispose();
        }

        Menu.windows = windows.ToArray();
    }

    internal static void Draw()
    {
        if (BeginMainMenuBar())
        {
            if (BeginMenu("Window"))
            {
                foreach (var window in windows)
                    if (MenuItem(window.Key))
                        Activator.CreateInstance(window.Value);

                EndMenu();
            }

#if DEBUG
            if (BeginMenu("Debug"))
            {
                if (BeginMenu("Test Log"))
                {
                    if (MenuItem("Message"))
                        Log("Test Message");

                    if (MenuItem("Warning"))
                        LogWarning("Test Warning");

                    if (MenuItem("Error"))
                        LogError("Test Error");

                    EndMenu();
                }

                EndMenu();
            }
#endif

            EndMainMenuBar();
        }
    }
}
