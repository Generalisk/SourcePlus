using Raylib_cs;
using rlImGui_cs;
using SourcePlus.Editor;

using static ImGuiNET.ImGui;
using static Raylib_cs.Raylib;

// Temporary path - will add a way to properly set project paths later
ProjectPath = "TestProject";

// Initialize Window
SetConfigFlags(ConfigFlags.ResizableWindow);

InitWindow(1280, 720, "Source+");

var icon = LoadImage("Content/ApplicationIcon.png");
SetWindowIcon(icon);

rlImGui.Setup(true, true);

Menu.Init();

// Main application loop
while (!WindowShouldClose())
{
    // Update
    for (int i = 0; i < Windows.Count; i++)
    {
        Windows[i].UpdateInternal(out bool closed);

        if (closed)
            i--;
    }

    // Draw
    BeginDrawing();
    ClearBackground(Color.Black);

    rlImGui.Begin();

    Menu.Draw();

    DockSpaceOverViewport();

    foreach (var window in Windows)
        window.DrawInternal();

    rlImGui.End();

    DrawFPS(10, 24);
    EndDrawing();
}

// Application closing - unload everything
while (Windows.Count > 0)
    Windows[0].Dispose();

rlImGui.Shutdown();

CloseWindow();

UnloadImage(icon);
