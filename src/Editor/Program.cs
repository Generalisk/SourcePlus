using rlImGui_cs;

using static Raylib_cs.Raylib;

InitWindow(1280, 720, "Source+");

rlImGui.Setup(true, true);

while (!WindowShouldClose())
{
    BeginDrawing();
    EndDrawing();
}

rlImGui.Shutdown();

CloseWindow();
