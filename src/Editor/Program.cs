using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using SourcePlus.Editor;
using SourcePlus.Editor.Windows;
using System.Numerics;

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


var viewport = new ImGuiViewport();
viewport.WorkPos = new Vector2(0, Menu.HEIGHT);
viewport.WorkSize = new Vector2(GetScreenWidth(), GetScreenHeight() - Menu.HEIGHT);

ImGuiViewportPtr viewportPtr;
unsafe
{
    viewportPtr = new ImGuiViewportPtr(&viewport);
}

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

    DockSpaceOverViewport(0, viewportPtr);

    foreach (var window in ActiveWindows)
        window.DrawInternal();

    if (ActivePopup != null)
        ActivePopup.DrawInternal();

    var io = GetIO();
    if (io.WantSaveIniSettings)
    {
        WindowHandler.SaveState();
        io.WantSaveIniSettings = false;
    }

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
