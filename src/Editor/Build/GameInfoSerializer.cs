using System.Collections.Generic;
using System.IO;
using ValveKeyValue;

namespace SourcePlus.Editor.Build;

internal static class GameInfoSerializer
{
    public static void Serialize(string file)
    {
        var stream = File.OpenWrite(file);
        Serialize(stream);
        stream.Dispose();
    }

    public static void Serialize(Stream stream)
    {
        var serializer = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
        serializer.Serialize(stream, Generate());
    }

    private static KVObject Generate()
    {
        var objects = new List<KVObject>();

        // TODO: Add more properties
        objects.Add(new KVObject("game", ProjectInfo.Instance.Name));
        objects.Add(new KVObject("title", ProjectInfo.Instance.Name));
        objects.Add(new KVObject("developer", ProjectInfo.Instance.Developer));

        return new KVObject("GameInfo", objects);
    }
}
