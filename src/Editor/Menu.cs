using static ImGuiNET.ImGui;

namespace SourcePlus.Editor;

internal static class Menu
{
    public static void Draw()
    {
        if (BeginMainMenuBar())
        {
            // Placeholder - for testing purposes
            if (BeginMenu("Example"))
            {
                if (MenuItem("Test"))
                    Console.WriteLine("Test Successful!");

                EndMenu();
            }

            EndMainMenuBar();
        }
    }
}
