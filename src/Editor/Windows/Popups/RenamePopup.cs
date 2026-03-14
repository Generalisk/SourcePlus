using System;
using System.IO;
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

    private Action<string> callback;

    internal RenamePopup(string path, Action<string> callback) : base()
    {
        this.callback = callback;

        FileSystemInfo info;

        if (Directory.Exists(path))
            info = new DirectoryInfo(path);
        else
            info = new FileInfo(path);

        if (!info.Exists)
        {
            LogError("Failed to open Rename popup");
            Dispose();
            return;
        }

        if (Directory.Exists(path))
        {
            var dirInfo = (DirectoryInfo)info;

            if (dirInfo.Parent == null)
            {
                LogError("Failed to open Rename popup");
                Dispose();
                return;
            }

            directory = dirInfo.Parent.FullName;
        }
        else
        {
            var fileInfo = (FileInfo)info;

            if (fileInfo.Directory == null)
            {
                LogError("Failed to open Rename popup");
                Dispose();
                return;
            }

            directory = fileInfo.Directory.FullName;
        }

        extension = info.Extension;

        name = info.Name;
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

        if (Directory.Exists(oldPath))
            Directory.Move(oldPath, newPath);
        else File.Move(oldPath, newPath);

        callback.Invoke(newName);

        Dispose();
    }

    public bool CanRename() => !string.IsNullOrWhiteSpace(newName) &&
        !File.Exists(Path.Combine(directory, newName + extension));
}
