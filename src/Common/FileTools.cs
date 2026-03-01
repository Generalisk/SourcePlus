using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace SourcePlus;

internal static class FileTools
{
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
    /// Deletes a file by sending it to the users recycling bin instead of permanently deleting it
    /// </summary>
    /// <param name="path">The path of the file you want to delete</param>
    public static void RecycleFile(string path) =>
        FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);

    /// <summary>
    /// Deletes a directory by sending it to the users recycling bin instead of permanently deleting it
    /// </summary>
    /// <param name="path">The path of the directory you want to delete</param>
    public static void RecycleDirectory(string path) =>
        FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
}
