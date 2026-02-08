using ImGuiNET;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows;

internal class ContentBrowserWindow : Window
{
    public override string Name { get; } = "Content Browser";

    public override ImGuiWindowFlags Flags { get; set; }
        = ImGuiWindowFlags.NoScrollbar;

    public override Vector2 MinSize { get; set; } = new Vector2(640, 240);

    private string Path { get => ProjectPath + "/" + path; }
    private string path = "game";

    protected override void Update()
    {
        if (!Directory.Exists(ProjectPath + "/game"))
            Directory.CreateDirectory(ProjectPath + "/game");

        if (!Directory.Exists(ProjectPath + "/src"))
            Directory.CreateDirectory(ProjectPath + "/src");
    }

    internal override void DrawInternal()
    {
        PushStyleVarY(ImGuiStyleVar.WindowPadding, 0);
        base.DrawInternal();
        PopStyleVar();
    }

    protected override void Draw()
    {
        var cursorPos = GetCursorPos();
        var windowHeight = GetWindowHeight() - cursorPos.Y;

        // Draw Sidebar
        if (BeginChild("contentbrowser_side", new Vector2(200, windowHeight), ImGuiChildFlags.AlwaysUseWindowPadding))
        {
            DrawSidePanelDirectory("game");
            DrawSidePanelDirectory("src");
        }

        EndChild();

        cursorPos.X += 200;
        SetCursorPos(cursorPos);

        var windowWidth = GetWindowWidth() - cursorPos.X;

        // Draw Main
        if (BeginChild("contentbrowser_main", new Vector2(windowWidth, windowHeight), ImGuiChildFlags.AlwaysUseWindowPadding))
        {
            var dirs = Directory.GetDirectories(Path);
            var files = Directory.GetFiles(Path);

            foreach (var dir in dirs)
            {
                if (Button(new DirectoryInfo(dir).Name))
                    path = dir.Substring(ProjectPath.Length + 1);
            }

            foreach (var file in files)
            {
                if (Button(new FileInfo(file).Name))
                    FileTools.OpenFile(file);
            }
        }
        EndChild();
    }

    private void DrawSidePanelDirectory(string directory)
    {
        if (Button(new DirectoryInfo(directory).Name))
            path = directory;

        var dirs = Directory.GetDirectories(ProjectPath + "/" + directory);

        if (!dirs.Any()) return;

        if (BeginChild(directory, Vector2.Zero,
            ImGuiChildFlags.AutoResizeX | ImGuiChildFlags.AutoResizeY |
            ImGuiChildFlags.AlwaysAutoResize | ImGuiChildFlags.AlwaysUseWindowPadding))
        {
            foreach (var dir in dirs)
            {
                var path = dir.Substring(ProjectPath.Length + 1);
                DrawSidePanelDirectory(path);
            }
        }
        EndChild();
    }
}
