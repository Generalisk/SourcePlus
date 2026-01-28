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
    foreach (var window in Windows)
        window.Update();

    BeginDrawing();
    ClearBackground(Color.Black);

    rlImGui.Begin();

    Menu.Draw();

    foreach (var window in Windows)
    {
        SetNextWindowSizeConstraints(window.MinSize, window.MaxSize);
        if (Begin(window.Name, window.Flags))
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
