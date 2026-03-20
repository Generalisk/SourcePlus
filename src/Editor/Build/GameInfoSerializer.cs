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

    private const string GAME_INFO_PATH = "|gameinfo_path|";
    private const string APPID_PATH = "|appid_{0}|";

    private static KVObject Generate()
    {
        var projectinfo = ProjectInfo.Instance;

        var gameinfo = new List<KVObject>();
        gameinfo.Add(new KVObject("game", projectinfo.Name));
        gameinfo.Add(new KVObject("type", projectinfo.IsMultiplayer
            ? "multiplayer_only" : "singleplayer_only"));

        gameinfo.Add(new KVObject("developer", projectinfo.Developer));

        var fileSystem = new List<KVObject>();
        fileSystem.Add(new KVObject("appid", projectinfo.AppID));

        var searchPaths = new List<KVObject>();
        searchPaths.Add(new KVObject("game+mod+custom_mod", GAME_INFO_PATH + "custom/*"));
        searchPaths.Add(new KVObject("mod+mod_write", GAME_INFO_PATH + "."));
        searchPaths.Add(new KVObject("game+game_write", GAME_INFO_PATH + "."));
        searchPaths.Add(new KVObject("default_write_path", GAME_INFO_PATH + "."));
        searchPaths.Add(new KVObject("gamebin", GAME_INFO_PATH + "bin"));
        // TODO: Impliment search paths for additional libraries
        searchPaths.Add(new KVObject("game+download", GAME_INFO_PATH + "download"));

        fileSystem.Add(new KVObject("SearchPaths", searchPaths));
        gameinfo.Add(new KVObject("FileSystem", fileSystem));

        return new KVObject("GameInfo", gameinfo);
    }
}
