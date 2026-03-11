using ImGuiNET;
using Raylib_cs;
using SourcePlus.Editor.Windows.Popups;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor;

internal class ProgressBar : Popup
{
    public override string Title { get; } = "";

    public override Vector2 Size => new Vector2(480, 64);

    private string text;
    private float progress;

    private bool showCancelButton;
    private bool cancelled;

    ProgressBar(string title, string text, float progress,
        bool showCancelButton, bool cancelled = false) : base()
    {
        Title = title;
        this.text = text;
        this.progress = progress;

        this.showCancelButton = showCancelButton;
        this.cancelled = cancelled;
    }

    internal override void DrawInternal()
    {
        var screenSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        SetNextWindowSize(screenSize);
        SetNextWindowPos(Vector2.Zero);
        SetNextWindowBgAlpha(0.42f);
        if (Begin("popup_background", //ImGuiWindowFlags.Popup |
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove |
            ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoSavedSettings))
        { }
        End();

        SetNextWindowFocus();
        SetNextWindowSize(Size);
        SetNextWindowPos(Raylib.GetScreenCenter() - (Size / 2));
        if (Begin(Title, //ImGuiWindowFlags.Popup |
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove |
            ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoSavedSettings |
            ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            Draw();
        End();
    }

    protected override void Draw()
    {
        ProgressBar(progress, new Vector2(464, 16));

        Text(text);

        if (showCancelButton)
        {
            var _cancelled = cancelled;

            if (_cancelled)
                BeginDisabled();

            SameLine();

            SetCursorPosX(GetWindowWidth() - 58);
            SetCursorPosY(GetCursorPosY() - 3);

            if (Button("Cancel"))
                cancelled = true;

            if (_cancelled)
                EndDisabled();
        }
    }

    private static bool Draw(string title, string text,
        float progress, bool showCancelButton)
    {
        var output = false;

        if (IsActivePopup())
        {
            var progessBar = (ProgressBar?)ActivePopup;

            if (progessBar != null) output = progessBar.cancelled;
        }

        new ProgressBar(title, text, progress, showCancelButton, output);

        return output;
    }

    /// <summary>
    /// Displays or updates a progress bar
    /// </summary>
    /// <param name="title">The progress bar title; appears in the popup title</param>
    /// <param name="info">Additional text; appears below the progress bar</param>
    /// <param name="progress">The progress to display, ranging from 0 to 1</param>
    public static void Draw(string title, string info, float progress)
        => Draw(title, info, progress, false);

    /// <summary>
    /// Displays or updates a progress bar; can be canceled
    /// </summary>
    /// <param name="title">The progress bar title; appears in the popup title</param>
    /// <param name="info">Additional text; appears below the progress bar</param>
    /// <param name="progress">The progress to display, ranging from 0 to 1</param>
    /// <returns>true if the cancel button has been clicked; otherwise false</returns>
    public static bool DrawCancelable(string title, string info, float progress)
        => Draw(title, info, progress, true);


    /// <summary>
    /// Closes the active progress bar
    /// </summary>
    public static void Clear()
    {
        if (IsActivePopup())
            ActivePopup = null;
    }

    /// <summary>
    /// Checks if a progress bar is the active popup
    /// </summary>
    /// <returns>true if the active popup is a progress bar, false otherwise</returns>
    private static bool IsActivePopup() =>
        ActivePopup != null && ActivePopup.GetType() == typeof(ProgressBar);
}
