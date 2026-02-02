using ImGuiNET;
using System.Numerics;

namespace SourcePlus.Editor.Windows;

internal class ConsoleWindow : Window
{
    public override string Name { get; } = "Console";

    public override Vector2 MinSize { get; set; } = new Vector2(480, 240);

    protected override void Draw()
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

            ImGui.TextColored(color, log.Message);
        }
    }
}
