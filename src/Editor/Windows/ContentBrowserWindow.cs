using ImGuiNET;
using SourcePlus.Editor.Windows.Popups;
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

    private List<string> expanded = new List<string>() { "game", "src" };

    private string? selected = null;

    protected override void Init()
    {
        base.Init();
        Update();
    }

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
            Text(Path + "/" + selected);
            Separator();

            var dirs = Directory.GetDirectories(Path);
            var files = Directory.GetFiles(Path);

            foreach (var dir in dirs)
            {
                var name = new DirectoryInfo(dir).Name;

                if (Button(name)) SelectItem(name);
            }

            foreach (var file in files)
            {
                var name = new FileInfo(file).Name;

                if (Button(name)) SelectItem(name);
            }

            if (IsWindowHovered() && IsMouseClicked(ImGuiMouseButton.Left))
                selected = null;

            // Context menu
            if (BeginPopupContextWindow())
            {
                if (MenuItem("Open in Explorer"))
                    FileTools.OpenPath(Path);

                Separator();

                if (selected == null || !File.Exists(Path + "/" + selected))
                    BeginDisabled();

                if (MenuItem("Rename"))
                    new RenamePopup(Path + "/" + selected,
                        (string newName) => { selected = newName; });

                if (MenuItem("Delete"))
                    new AskPopup("Delete file?",
                        string.Format("Are you sure you want to delete {0}?", selected),
                        (bool yes) => {
                            var path = Path + "/" + selected;

                            if (!yes) return;

                            if (Directory.Exists(path))
                                FileTools.RecycleDirectory(path);
                            else FileTools.RecycleFile(path);

                            selected = null;
                        });

                if (selected == null || !File.Exists(Path + "/" + selected))
                    EndDisabled();

                EndPopup();
            }
        }
        EndChild();
    }

    private void DrawSidePanelDirectory(string directory)
    {
        directory = directory.Replace("\\", "/");

        var isExpanded = expanded.Contains(directory);

        PushID(new DirectoryInfo(directory).Name);
        if (Button(isExpanded ? "-" : "+"))
            if (isExpanded)
                expanded.Remove(directory);
            else
                expanded.Add(directory);
        PopID();

        SameLine();

        if (Button(new DirectoryInfo(directory).Name))
            OpenDirectory(directory);

        if (!isExpanded) return;

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

    public void SelectItem(string? item)
    {
        if (selected == item && item != null)
            if (Directory.Exists(Path + "/" + item))
                OpenDirectory(path + "/" + item);
            else
                FileTools.OpenPath(Path + "/" + item);
        else
            selected = item;
    }

    public void OpenDirectory(string directory)
    {
        path = directory;
        selected = null;
    }
}
