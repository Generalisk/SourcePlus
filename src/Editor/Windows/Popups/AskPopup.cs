using System;
using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows.Popups;

internal class AskPopup : Popup
{
    public override string Title { get; } = "";

    public override Vector2 Size { get; } = new Vector2(0, 0);

    private string message = "";

    private string yes = "Yes";
    private string no = "No";

    private Action<bool> callback;

    public AskPopup(string title, string message, Action<bool> callback) : base()
    {
        Title = title;
        this.message = message;

        this.callback = callback;

        Size = CalcTextSize(message, 250f);
        Size += new Vector2(16, 56);
    }

    public AskPopup(string title, string message, string yes, string no, Action<bool> callback) : base()
    {
        Title = title;
        this.message = message;
        this.yes = yes;
        this.no = no;

        this.callback = callback;

        Size = CalcTextSize(message, 250f);
        Size += new Vector2(16, 56);
    }

    protected override void Draw()
    {
        TextWrapped(message);

        var buttonSizes = CalcTextSize(yes).X + CalcTextSize(no).X;
        SetCursorPosX(GetWindowSize().X - buttonSizes - 32);

        if (Button(yes))
            Accept();

        SameLine();

        if (Button(no))
            Decline();
    }

    public void Accept()
    {
        callback.Invoke(true);
        Dispose();
    }

    public void Decline()
    {
        callback.Invoke(false);
        Dispose();
    }
}
