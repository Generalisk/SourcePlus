using Raylib_cs;
using rlImGui_cs;
using SourcePlus.Editor;
using SourcePlus.Editor.Windows;

using static ImGuiNET.ImGui;
using static Raylib_cs.Raylib;

// Temporary path - will add a way to properly set project paths later
ProjectPath = "TestProject";

ProjectInfo.Load();

// Initialize Window
SetConfigFlags(ConfigFlags.ResizableWindow);

InitWindow(1280, 720, "Source+");
UpdateTitle();

var icon = LoadImage("Content/ApplicationIcon.png");
SetWindowIcon(icon);

SetExitKey(KeyboardKey.Null);

rlImGui.Setup(true, true);

WindowHandler.Init();
WindowHandler.LoadState();

// Main application loop
while (!WindowShouldClose())
{
    // Update
    for (int i = 0; i < ActiveWindows.Count; i++)
    {
        ActiveWindows[i].UpdateInternal(out bool closed);

        if (closed)
            i--;
    }

    if (ActivePopup != null)
        ActivePopup.UpdateInternal();

    // Draw
    BeginDrawing();
    ClearBackground(Color.Black);

    rlImGui.Begin();

    Menu.Draw();

    DockSpaceOverViewport();

    foreach (var window in ActiveWindows)
        window.DrawInternal();

    if (ActivePopup != null)
        ActivePopup.DrawInternal();

    rlImGui.End();

    EndDrawing();
}

// Application closing - unload everything
if (ActivePopup != null)
    ActivePopup.Dispose();

WindowHandler.SaveState();
WindowHandler.CloseAll();

rlImGui.Shutdown();

UnloadImage(icon);

CloseWindow();

ProjectInfo.Save();
