using Raylib_cs;
using rlImGui_cs;

using static Raylib_cs.Raylib;

InitWindow(1280, 720, "Source+");

rlImGui.Setup(true, true);

while (!WindowShouldClose())
{
    BeginDrawing();
    ClearBackground(Color.Black);
    DrawFPS(10, 10);
    EndDrawing();
}

rlImGui.Shutdown();

CloseWindow();
