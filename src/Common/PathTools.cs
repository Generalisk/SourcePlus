using System.Diagnostics;
using System.IO;

namespace SourcePlus;

public static class PathTools
{
    /// <summary>
    /// Opens path in default program; directories will open in file explorer
    /// </summary>
    /// <param name="path"></param>
    public static void OpenPath(string path)
    {
        Log("Opening \"{0}\"", path);
        path = Path.GetFullPath(path);

        Process.Start(new ProcessStartInfo()
        {
            // TODO: Test on devices that aren't Windows
#if PLATFORM_WINDOWS
            FileName = "explorer.exe",
            Arguments = "\"" + path + "\"",
#elif PLATOFRM_OSX
            FileName = "/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal",
            Arguments = "open \"" + path + "\"",
#elif PLATFORM_LINUX
            FileName = "/bin/bash",
            Arguments = "xdg-open \"" + path + "\"",
#endif
        });
    }

    /// <summary>
    /// Open URL in default web browser
    /// </summary>
    /// <param name="url">The web url i.e. https://example.com/</param>
    public static void OpenURL(string url)
    {
        Log("Opening \"{0}\" in default web browser", url);

        Process.Start(new ProcessStartInfo()
        {
            // TODO: Test on devices that aren't Windows
#if PLATFORM_WINDOWS
            FileName = url,
            UseShellExecute = true,
#elif PLATOFRM_OSX
            FileName = "open",
            Arguments = url,
#elif PLATFORM_LINUX
            FileName = "xdg-open",
            Arguments = url,
#endif
        });
    }
}
