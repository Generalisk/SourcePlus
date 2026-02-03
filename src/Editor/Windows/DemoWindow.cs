#if DEBUG
using ImGuiNET;

namespace SourcePlus.Editor.Windows;

internal class DemoWindow : Window
{
    public override string Name { get; } = "Demo Window";

    private bool open = true;

    internal override void UpdateInternal(out bool closed)
    {
        Update();

        closed = false;

        if (!open)
        {
            Dispose();
            closed = true;
            return;
        }
    }

    internal override void DrawInternal()
    {
        ImGui.SetNextWindowSizeConstraints(MinSize, MaxSize);
        ImGui.ShowDemoWindow(ref open);
    }

    protected override void Draw()
    {
        throw new NotImplementedException();
    }
}
#endif // DEBUG
