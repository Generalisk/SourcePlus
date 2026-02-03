#if DEBUG
using static ImGuiNET.ImGui;

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
        SetNextWindowSizeConstraints(MinSize, MaxSize);
        ShowDemoWindow(ref open);
    }

    protected override void Draw() =>
        throw new NotImplementedException();
}
#endif // DEBUG
