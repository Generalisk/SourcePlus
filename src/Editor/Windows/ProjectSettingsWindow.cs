using ImGuiNET;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows;

internal class ProjectSettingsWindow : Window
{
    public override string Name { get; } = "Project Settings";

    public override ImGuiWindowFlags Flags { get; set; }
        = ImGuiWindowFlags.NoScrollbar;

    public override Vector2 MinSize { get; set; } = new Vector2(480, 640);

    private byte tab = 0;

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
        if (BeginChild("projectsettings_sidebar", new Vector2(100, windowHeight)))
        {
            if (Button("All")) tab = 0;
            if (Button("General")) tab = 1;
#if DEBUG
            if (Button("Empty")) tab = 2;
#endif
        }
        EndChild();

        cursorPos.X += 100;
        SetCursorPos(cursorPos);

        var windowWidth = GetWindowWidth() - cursorPos.X;

        // Draw Main
        if (BeginChild("projectsettings_main"))
        {
            if (tab == 0 || tab == 1)
            {
                SetWindowFontScale(1.5f);
                Text("General Settings");
                SetWindowFontScale(1);

                var name = ProjectInfo.Instance.Name;
                if (InputText("Name", ref name, ushort.MaxValue))
                    ProjectInfo.Instance.Name = name;

                var developer = ProjectInfo.Instance.Developer;
                if (InputText("Developer", ref developer, ushort.MaxValue))
                    ProjectInfo.Instance.Developer = developer;

                Spacing();
            }
#if DEBUG
            if (tab == 0 || tab == 2)
            {
                SetWindowFontScale(1.5f);
                Text("This is an empty tab");
                SetWindowFontScale(1);

                Text("This is simply for testing purposes & will be removed later");

                Spacing();
            }
#endif
        }
        EndChild();
    }
}
