using Raylib_cs;
using rlImGui_cs;
using SourcePlus.Editor;

using static ImGuiNET.ImGui;
using static Raylib_cs.Raylib;

Menu.Init();

SetConfigFlags(ConfigFlags.ResizableWindow);

InitWindow(1280, 720, "Source+");

var icon = LoadImage("Content/ApplicationIcon.png");
SetWindowIcon(icon);

rlImGui.Setup(true, true);

while (!WindowShouldClose())
{
    for (int i = 0; i < Windows.Count; i++)
    {
        Windows[i].Update(out bool closed);

        if (closed)
            i--;
    }

    BeginDrawing();
    ClearBackground(Color.Black);

    rlImGui.Begin();

    Menu.Draw();

    DockSpaceOverViewport();

    foreach (var window in Windows)
    {
        SetNextWindowSizeConstraints(window.MinSize, window.MaxSize);
        if (Begin(window.Name, ref window.open, window.Flags))
            window.Draw();
        End();
    }

    rlImGui.End();

    DrawFPS(10, 10);
    EndDrawing();
}

rlImGui.Shutdown();

CloseWindow();

UnloadImage(icon);
