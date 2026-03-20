using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SourcePlus.Editor.Build;

internal static class BuildSystem
{
    /// <summary>
    /// Exports the project to a specified folder
    /// </summary>
    /// <param name="outputFolder">The directory to export the project to, should be empty</param>
    public static void Export(string outputFolder)
    {
        if (Directory.Exists(outputFolder))
            if (Directory.GetDirectories(outputFolder).Length > 0
                || Directory.GetFiles(outputFolder).Length > 0)
                LogWarning("Output folder ({0}) is not empty!", outputFolder);

        var sw = Stopwatch.StartNew();

        var libraryName = ProjectInfo.Instance.Library;

        // Export content files
        var files = Directory.GetFiles(ProjectPath + "/game", "*", SearchOption.AllDirectories);
        files = files.Select(x => x.Substring((ProjectPath + "/game").Length + 1)).ToArray();

        // TODO: Thread export function so that the application
        // doesn't hang while the project is exporting
        foreach (var file in files)
        {
            if (!Directory.Exists(outputFolder + "/" + file + "/../"))
                Directory.CreateDirectory(outputFolder + "/" + file + "/../");

            File.Copy(ProjectPath + "/game/" + file, outputFolder + "/" + file, true);
        }

        // Generate gameinfo.txt
        if (!Directory.Exists(outputFolder + "/" + libraryName))
            Directory.CreateDirectory(outputFolder + "/" + libraryName);

        GameInfoSerializer.Serialize(outputFolder + "/" + libraryName + "/gameinfo.txt");

        // Finish up
        sw.Stop();
        Log("Successfully exported project in {0}", sw.Elapsed);

        FileTools.OpenPath(outputFolder);
    }
}
