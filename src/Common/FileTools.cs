using System.Diagnostics;

namespace SourcePlus;

internal static class FileTools
{
    public static void OpenFile(string path)
    {
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
            WindowStyle = ProcessWindowStyle.Hidden,
        });
    }
}
