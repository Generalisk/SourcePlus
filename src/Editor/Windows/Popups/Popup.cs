using ImGuiNET;
using Raylib_cs;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows.Popups;

// yes IK ImGui has a built-in popup system,
// but I just couldn't get it to work so we're doing this the hard way.
// but at least we have more control and flexibility with this system
// -Generalisk, 27/02/26
internal abstract class Popup : IDisposable
{
    public abstract string Title { get; }

    public abstract Vector2 Size { get; }

    private bool open = true;

    public Popup()
    {
        if (ActivePopup != null)
        {
            LogError("Cannot open popup. A popup is already active.");
            Dispose();
            return;
        }

        ActivePopup = this;

        Init();
    }

    /// <summary>
    /// Closes the popup & unloads it from memory
    /// </summary>
    public void Dispose()
    {
        Shutdown();

        if (ActivePopup != null)
            ActivePopup = null;
    }

    internal virtual void UpdateInternal()
    {
        Update();

        if (!open)
        {
            Dispose();
            return;
        }
    }

    internal virtual void DrawInternal()
    {
        var screenSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        SetNextWindowSize(screenSize);
        SetNextWindowPos(Vector2.Zero);
        SetNextWindowBgAlpha(0.42f);
        if (Begin("popup_background", ref open, //ImGuiWindowFlags.Popup |
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove |
            ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoSavedSettings))
        { }
        End();

        SetNextWindowFocus();
        SetNextWindowSize(Size);
        SetNextWindowPos(Raylib.GetScreenCenter() - (Size / 2));
        if (Begin(Title, ref open, //ImGuiWindowFlags.Popup |
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove |
            ImGuiWindowFlags.NoCollapse |  ImGuiWindowFlags.NoSavedSettings |
            ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            Draw();
        End();
    }

    protected virtual void Init() { }
    protected virtual void Shutdown() { }
    protected virtual void Update() { }
    protected abstract void Draw();
}
