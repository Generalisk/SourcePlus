using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace SourcePlus.Editor.Build;

internal static class BuildSystem
{
    /// <summary>
    /// Builds and exports the project to a specified folder
    /// </summary>
    /// <param name="outputFolder">The directory to build the project to, should be empty</param>
    public static void Build(string outputFolder)
    {
        // Run thread
        var thread = new Thread(() =>
            BuildNoThread(outputFolder));
        thread.IsBackground = true;
        thread.Start();
    }

    private static void BuildNoThread(string outputFolder)
    {
        ProgressBar.Draw("Building", "Hold on...", 0);

        if (Directory.Exists(outputFolder))
            if (Directory.GetDirectories(outputFolder).Length > 0
                || Directory.GetFiles(outputFolder).Length > 0)
                LogWarning("Output folder ({0}) is not empty!", outputFolder);

        var sw = Stopwatch.StartNew();

        var libraryName = ProjectInfo.Instance.Library;

        // Export content files
        ProgressBar.Draw("Exporting", "Hold on...", 0);

        var files = Directory.GetFiles(ProjectPath + "/game", "*", SearchOption.AllDirectories);
        files = files.Select(x => x.Substring((ProjectPath + "/game").Length + 1)).ToArray();

        for (int i = 0; i < files.Length; i++)
        {
            var file = files[i];

            ProgressBar.Draw("Exporting", file, (1f / files.Length) * i);

            if (!Directory.Exists(outputFolder + "/" + file + "/../"))
                Directory.CreateDirectory(outputFolder + "/" + file + "/../");

            File.Copy(ProjectPath + "/game/" + file, outputFolder + "/" + file, true);
        }

        // Generate gameinfo.txt
        ProgressBar.Draw("Exporting", "Generating gameinfo.txt", 1);

        if (!Directory.Exists(outputFolder + "/" + libraryName))
            Directory.CreateDirectory(outputFolder + "/" + libraryName);

        GameInfoSerializer.Serialize(outputFolder + "/" + libraryName + "/gameinfo.txt");

        // Finish up
        sw.Stop();
        Log("Successfully exported project in {0}", sw.Elapsed);

        ProgressBar.Clear();

        FileTools.OpenPath(outputFolder);
    }
}
