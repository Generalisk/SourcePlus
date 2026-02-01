using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using System.Numerics;

using static ImGuiNET.ImGui;
using static Raylib_cs.Raylib;

namespace SourcePlus.Editor.Windows;

internal class View3DWindow : Window
{
    public override string Name { get; } = "3D View";

    public override ImGuiWindowFlags Flags { get; set; }
        = ImGuiWindowFlags.NoScrollbar;

    public override Vector2 MinSize { get; set; } = new Vector2(480, 360);

    private Camera3D camera;
    private RenderTexture2D screen;

    protected override void Init()
    {
        camera = new Camera3D()
        {
            Position = Vector3.Zero,
            Target = new Vector3(0, 0, 10),
            Up = new Vector3(0, 1, 0),
            Projection = CameraProjection.Perspective,
            FovY = 60,
        };

        screen = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());
    }

    protected override void Shutdown()
    {
        UnloadRenderTexture(screen);
    }

    protected override void Update()
    {
        UpdateCamera(ref camera, CameraMode.Free);
    }

    internal override void DrawInternal()
    {
        PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        base.DrawInternal();
        PopStyleVar();
    }

    protected override void Draw()
    {
        // Check if the window has been resized
        if (IsResized())
        {
            // Resize the screen texture
            UnloadRenderTexture(screen);

            var width = Convert.ToInt32(GetWindowWidth());
            var height = Convert.ToInt32(GetWindowHeight());
            screen = LoadRenderTexture(width, height);
        }

        // Draw screen texture
        BeginTextureMode(screen);

        ClearBackground(Color.Black);

        BeginMode3D(camera);

        DrawGrid(64, 1);

        EndMode3D();

        DrawFPS(10, 10);

        EndTextureMode();

        // Draw the screen texture onto the window
        rlImGui.ImageRenderTextureFit(screen, true);
    }

    private bool IsResized()
    {
        var screenSize = new Vector2(screen.Texture.Width, screen.Texture.Height);
        return screenSize != GetWindowSize();
    }
}
