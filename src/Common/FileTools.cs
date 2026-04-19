using Microsoft.VisualBasic.FileIO;

namespace SourcePlus;

public static class FileTools
{

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
