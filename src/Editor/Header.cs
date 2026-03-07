using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using System.Numerics;

using static ImGuiNET.ImGui;
using static Raylib_cs.Raylib;

namespace SourcePlus.Editor;

internal static class Header
{
    internal const int HEIGHT = 36;
    private const int LOGO_HEIGHT = 32;

    private static Texture2D logo;

    internal static void Init() =>
        logo = LoadTexture("../resources/sourceplus_logo.png");

    internal static void Shutdown() => UnloadTexture(logo);

    internal static void Draw()
    {
        SetNextWindowPos(new Vector2(0, Menu.HEIGHT));
        SetNextWindowSize(new Vector2(GetScreenWidth(), HEIGHT));

        if (Begin("header", ImGuiWindowFlags.NoTitleBar |
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove |
            ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse |
            ImGuiWindowFlags.NoSavedSettings))
        {
            SetCursorPosY(GetCursorPosY() - 5);
            rlImGui.ImageSize(logo, CalculateWidth(), LOGO_HEIGHT);

            End();
        }
    }

    private static int CalculateWidth()
    {
        var height = (float)LOGO_HEIGHT / logo.Height;
        var width = logo.Width * height;
        return Convert.ToInt32(width);
    }
}
