using ImGuiNET;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows;

internal class ConsoleWindow : Window
{
    public override string Name { get; } = "Console";

    public override Vector2 MinSize { get; set; } = new Vector2(480, 240);

    protected override void Draw()
    {
        if (Button("Clear"))
            Logs.Clear();

        if (BeginChild("logList", GetWindowSize() - (GetCursorPos() + new Vector2(0, 8))))
        {
            foreach (var log in Logs)
            {
                Vector4 color;
                switch (log.Type)
                {
                    case LogType.Warning: color = new Vector4(1, 1, 0, 1); break;
                    case LogType.Error: color = new Vector4(1, 0, 0, 1); break;
                    default: color = new Vector4(1, 1, 1, 1); break;
                }

                TextColored(color, log.Message);
            }

            EndChild();
        }
    }
}
