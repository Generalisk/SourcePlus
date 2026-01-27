using Raylib_cs;
using rlImGui_cs;
using SourcePlus.Editor;

using static Raylib_cs.Raylib;

SetConfigFlags(ConfigFlags.ResizableWindow);

InitWindow(1280, 720, "Source+");

var icon = LoadImage("Content/ApplicationIcon.png");
SetWindowIcon(icon);

rlImGui.Setup(true, true);

while (!WindowShouldClose())
{
    BeginDrawing();
    ClearBackground(Color.Black);

    rlImGui.Begin();
    Menu.Draw();
    rlImGui.End();

    DrawFPS(10, 10);
    EndDrawing();
}

rlImGui.Shutdown();

CloseWindow();

UnloadImage(icon);
