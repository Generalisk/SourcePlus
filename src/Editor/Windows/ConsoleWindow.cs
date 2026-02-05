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

    private bool collapse = false;

    protected override void Draw()
    {
        if (Button("Clear"))
            Logs.Clear();

        SameLine();

        Checkbox("Collapse", ref collapse);

        SameLine();

        var messageCount = Logs.Where(x => x.Type == LogType.Message).Count();
        Checkbox(string.Format("Messages ({0})", messageCount), ref showMessages);

        SameLine();

        var warningCount = Logs.Where(x => x.Type == LogType.Warning).Count();
        Checkbox(string.Format("Warnings ({0})", warningCount), ref showWarnings);

        SameLine();

        var errorCount = Logs.Where(x => x.Type == LogType.Error).Count();
        Checkbox(string.Format("Errors ({0})", errorCount), ref showErrors);

        if (BeginChild("logList", GetWindowSize() - (GetCursorPos() + new Vector2(0, 8))))
        {
            var logs = new List<string>();
            var timestamps = new List<DateTime>();
            var types = new List<LogType>();

            if (collapse)
            {
                foreach (var log in Logs)
                {
                    int index = -1;

                    if (logs.Count > 0)
                    {
                        while (true)
                        {
                            index = logs.IndexOf(log.Message, Math.Min(index + 1, logs.Count));

                            if (index < 0) break;

                            if (types[index] == log.Type) break;
                        }
                    }

                    if (index >= 0)
                    {
                        timestamps[index] = log.Time;
                    }
                    else
                    {
                        logs.Add(log.Message);
                        timestamps.Add(log.Time);
                        types.Add(log.Type);
                    }
                }
            }
            else
            {
                logs = Logs.Select(x => x.Message).ToList();
                timestamps = Logs.Select(x => x.Time).ToList();
                types = Logs.Select(x => x.Type).ToList();
            }

            for (int i = 0; i < logs.Count; i++)
            {
                Vector4 color = new Vector4(1, 1, 1, 1);
                switch (types[i])
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

                var hour = timestamps[i].Hour.ToString();
                var minute = timestamps[i].Minute.ToString();
                var second = timestamps[i].Second.ToString();

                hour = hour.Length > 1 ? hour : "0" + hour;
                minute = minute.Length > 1 ? minute : "0" + minute;
                second = second.Length > 1 ? second : "0" + second;

                var time = string.Format("{0}:{1}:{2}", hour, minute, second);

                TextColored(color, string.Format("[{1}] {0}", logs[i], time));
            }

            EndChild();
        }
    }
}
