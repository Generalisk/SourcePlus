using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using SourcePlus.Editor;
using SourcePlus.Editor.Windows;
using System;
using System.IO;
using System.Numerics;

using static ImGuiNET.ImGui;
using static Raylib_cs.Raylib;

static class Program
{
    private static Image icon;

    /// <summary>
    /// Application entry point
    /// </summary>
    static void Main()
    {
        // Temporary path - will add a way to properly set project paths later
        ProjectPath = "TestProject";

        Init();

        // Main application loop
        while (!WindowShouldClose())
        {
            Update();
            Draw();
        }

        Shutdown();
    }

    /// <summary>
    /// Called when the application is first initialized
    /// </summary>
    static void Init()
    {
        // Set working directory - make sure the output stays consistent
        // regardless of where the application was called from
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory + "/../");

        // Load project
        ProjectInfo.Load();

        // Initialize Window
        SetConfigFlags(ConfigFlags.ResizableWindow);

        InitWindow(1280, 720, "Source+");
        UpdateTitle();

        icon = LoadImage("resources/sourceplus_icon.png");
        SetWindowIcon(icon);

        SetExitKey(KeyboardKey.Null);

        rlImGui.Setup(true, true);

        Header.Init();

        WindowHandler.Init();
        WindowHandler.LoadState();
    }

    /// <summary>
    /// Called when the application is closing
    /// </summary>
    static void Shutdown()
    {
        // Application closing - unload everything

        // Unload & close application window
        ActivePopup?.Dispose();

        Header.Shutdown();

        WindowHandler.SaveState();
        WindowHandler.CloseAll();

        rlImGui.Shutdown();

        UnloadImage(icon);

        CloseWindow();

        // Save project
        ProjectInfo.Save();
    }

    /// <summary>
    /// Called every frame - used for general update logic
    /// </summary>
    static void Update()
    {
        // Update all active windows
        for (int i = 0; i < ActiveWindows.Count; i++)
        {
            ActiveWindows[i].UpdateInternal(out bool closed);

            if (closed)
                i--;
        }

        // Update active popup (if applicable)
        ActivePopup?.UpdateInternal();

        // Save window state
        var io = GetIO();
        if (io.WantSaveIniSettings)
        {
            WindowHandler.SaveState();
            io.WantSaveIniSettings = false;
        }
    }

    /// <summary>
    /// Called every frame - used for drawing onto the screen
    /// </summary>
    static void Draw()
    {
        // Generate Window Viewport
        var viewport = new ImGuiViewport();
        viewport.WorkPos = new Vector2(0, Menu.HEIGHT + Header.HEIGHT);
        viewport.WorkSize = new Vector2(GetScreenWidth(), GetScreenHeight() - Menu.HEIGHT - Header.HEIGHT - Footer.HEIGHT);

        ImGuiViewportPtr viewportPtr;
        unsafe
        {
            viewportPtr = new ImGuiViewportPtr(&viewport);
        }

        // Begin drawing
        BeginDrawing();
        ClearBackground(Color.Black);
        rlImGui.Begin();

        // Draw UI
        Menu.Draw();
        Header.Draw();
        Footer.Draw();

        DockSpaceOverViewport(0, viewportPtr);

        // Draw all active windows
        foreach (var window in ActiveWindows)
            window.DrawInternal();

        // Draw popup (if applicable)
        ActivePopup?.DrawInternal();

        // Finish drawing
        rlImGui.End();
        EndDrawing();
    }
}
