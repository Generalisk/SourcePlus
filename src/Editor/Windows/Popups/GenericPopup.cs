using System.Numerics;

using static ImGuiNET.ImGui;

namespace SourcePlus.Editor.Windows.Popups;

internal class GenericPopup : Popup
{
    public override string Title { get; } = "";

    public override Vector2 Size { get; } = new Vector2(0, 0);

    private string message = "";

    public GenericPopup(string title, string message) : base()
    {
        Title = title;
        this.message = message;
        Size = CalcTextSize(message, 250f);
        Size += new Vector2(16, 56);
    }

    protected override void Draw()
    {
        TextWrapped(message);

        // TODO: Align this to the right
        if (Button("Ok"))
            Dispose();
    }
}
