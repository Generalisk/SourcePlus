using ImGuiNET;
using System.Numerics;

namespace SourcePlus.Editor.Windows;

internal abstract class Window
{
    public abstract string Name { get; }

    public virtual ImGuiWindowFlags Flags { get; set; } = ImGuiWindowFlags.None;

    public virtual Vector2 MinSize { get; set; } = Vector2.Zero;
    public virtual Vector2 MaxSize { get; set; } =
        new Vector2(ushort.MaxValue, ushort.MaxValue);

    public Window() => Global.Windows.Add(this);

    public virtual void Dispose() => Global.Windows.Remove(this);

    public virtual void Update() { }
    
    public abstract void Draw();
}
