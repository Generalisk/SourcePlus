using ImGuiNET;
using Raylib_cs;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor;

internal static class Footer
{
    internal const int HEIGHT = 24;

    internal static void Draw()
    {
        SetNextWindowPos(new Vector2(0, Raylib.GetScreenHeight() - HEIGHT));
        SetNextWindowSize(new Vector2(Raylib.GetScreenWidth(), HEIGHT));
        if (Begin("footer", ImGuiWindowFlags.NoTitleBar |
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove |
            ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse |
            ImGuiWindowFlags.NoSavedSettings))
        {
            // Draw last console log
            if (Logs.Any())
            {
                var log = Logs.Last();

                var timestamp = log.Time.ToTimestamp();

                var color = new Vector4(1, 1, 1, 1);
                switch (log.Type)
                {
                    case LogType.Warning:
                        color = new Vector4(1, 1, 0, 1);
                        break;
                    case LogType.Error:
                        color = new Vector4(1, 0, 0, 1);
                        break;
                }

                var text = string.Format("[{1}] {0}", log.Message, timestamp);

                SetCursorPosY(GetCursorPosY() - 2);
                TextColored(color, text);
            }

            End();
        }
    }
}
