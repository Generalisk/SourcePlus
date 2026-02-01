using ImGuiNET;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows;

/// <summary>
/// Core window object
/// </summary>
internal abstract class Window
{
    public abstract string Name { get; }

    public virtual ImGuiWindowFlags Flags { get; set; } = ImGuiWindowFlags.None;

    public virtual Vector2 MinSize { get; set; } = Vector2.Zero;
    public virtual Vector2 MaxSize { get; set; } =
        new Vector2(ushort.MaxValue, ushort.MaxValue);

    private bool open = true;

    public Window()
    {
        Global.Windows.Add(this);

        Init();
    }

    /// <summary>
    /// Closes the window & unloads it from memory
    /// </summary>
    public void Dispose()
    {
        Shutdown();

        Global.Windows.Remove(this);
    }

    internal virtual void UpdateInternal(out bool closed)
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

    internal virtual void DrawInternal()
    {
        SetNextWindowSizeConstraints(MinSize, MaxSize);
        if (Begin(Name, ref open, Flags))
            Draw();
        End();
    }

    protected virtual void Init() { }
    protected virtual void Shutdown() { }
    protected virtual void Update() { }
    protected abstract void Draw();
}
