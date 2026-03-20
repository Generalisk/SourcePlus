using System.IO;
using ValveKeyValue;

namespace SourcePlus.Editor;

internal class ProjectInfo
{
    public static ProjectInfo Instance { get => instance; }
    private static ProjectInfo instance = new ProjectInfo();

    private static string Path { get => ProjectPath + "/projectinfo.vdf"; }

    public string Name { get; set; } = "Untitled";
    public string Developer { get; set; } = "Me";
    public string Library { get; set; } = "untitled";

    public int AppID { get; set; } = 243750; // Source SDK 2013 Multiplayer

    public bool IsMultiplayer { get; set; } = true;

    ProjectInfo() { }

    public static void Load()
    {
        if (!File.Exists(Path)) return;

        var stream = File.OpenRead(Path);

        var serializer = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
        var data = serializer.Deserialize<ProjectInfo>(stream);

        stream.Dispose();

        instance = data;
    }

    public static void Save()
    {
        if (!Directory.Exists(Path + "/../"))
            Directory.CreateDirectory(Path + "/../");

        var stream = File.OpenWrite(Path);

        var serializer = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
        serializer.Serialize(stream, Instance, "ProjectInfo");

        stream.Dispose();
    }
}
