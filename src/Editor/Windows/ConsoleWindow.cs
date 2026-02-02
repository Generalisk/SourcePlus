using ImGuiNET;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows;

internal class ConsoleWindow : Window
{
    public override string Name { get; } = "Console";

    public override Vector2 MinSize { get; set; } = new Vector2(480, 240);

    private bool showMessages = true;
    private bool showWarnings = true;
    private bool showErrors = true;

    protected override void Draw()
    {
        if (Button("Clear"))
            Logs.Clear();

        SameLine();

        Checkbox("Messages", ref showMessages);

        SameLine();

        Checkbox("Warnings", ref showWarnings);

        SameLine();

        Checkbox("Errors", ref showErrors);

        if (BeginChild("logList", GetWindowSize() - (GetCursorPos() + new Vector2(0, 8))))
        {
            foreach (var log in Logs)
            {
                Vector4 color = new Vector4(1, 1, 1, 1);
                switch (log.Type)
                {
                    case LogType.Message:
                        if (!showMessages) continue;
                        break;
                    case LogType.Warning:
                        color = new Vector4(1, 1, 0, 1);
                        if (!showWarnings) continue;
                        break;
                    case LogType.Error:
                        color = new Vector4(1, 0, 0, 1);
                        if (!showErrors) continue;
                        break;
                }

                TextColored(color, log.Message);
            }

            EndChild();
        }
    }
}
