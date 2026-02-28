using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows.Popups;

internal class RenamePopup : Popup
{
    public override string Title => "Rename file";

    public override Vector2 Size => new Vector2(420, 72);

    private string directory = "";
    private string name = "";
    private string extension = "";

    private string newName = "";

    public RenamePopup(string path) : base()
    {
        var fileInfo = new FileInfo(path);

        if (fileInfo.Directory == null)
        {
            LogError("Failed to open Rename popup");
            Dispose();
            return;
        }

        directory = fileInfo.Directory.FullName;
        extension = fileInfo.Extension;
        name = fileInfo.Name;
        name = name.Substring(0, name.Length - extension.Length);
        newName = name;
    }

    protected override void Draw()
    {
        InputText(extension, ref newName, 64);

        SetCursorPosX(GetWindowSize().X - CalcTextSize("Ok").X - 72);

        var canRename = CanRename();

        if (!canRename)
            BeginDisabled();

        if (Button("Ok"))
            Rename();

        if (!canRename)
            EndDisabled();

        SameLine();

        if (Button("Cancel"))
            Dispose();
    }

    public void Rename()
    {
        if (!CanRename()) return;

        var oldPath = Path.Combine(directory, name + extension);
        var newPath = Path.Combine(directory, newName + extension);

        File.Move(oldPath, newPath);

        Dispose();
    }

    public bool CanRename() => !string.IsNullOrWhiteSpace(newName) &&
        !File.Exists(Path.Combine(directory, newName + extension));
}
