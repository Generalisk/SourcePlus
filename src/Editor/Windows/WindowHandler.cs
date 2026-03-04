using ImGuiNET;
using System.Reflection;
using ValveKeyValue;

namespace SourcePlus.Editor.Windows;

internal static class WindowHandler
{
    private static KeyValuePair<string, Type>[] windows = { };

    internal static void Init()
    {
        // Retrieve window menu buttons
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes().Where(x => IsValidType(x));

        var windows = new List<KeyValuePair<string, Type>>();

        foreach (var type in types)
        {
            if (type == null) continue;

            var window = (Window?)Activator.CreateInstance(type);

            if (window == null) continue;

            windows.Add(new KeyValuePair<string, Type>(window.Name, type));
            window.Dispose();
        }

        WindowHandler.windows = windows.ToArray();
    }

    public static string? GetName<T>() => GetName(typeof(T));

    public static string? GetName(Type type)
    {
        if (!IsValidType(type)) return null;

        var search = windows.Where(x => x.Value == type);

        if (search.Any())
            return search.First().Key;
        else
            return null;
    }

    public static Type? GetType(string name)
    {
        var search = windows.Where(x => x.Key.ToLower() == name.ToLower());

        if (search.Any())
            return search.First().Value;
        else
            return null;
    }

    public static string[] GetNames() =>
        windows.Select(x => x.Key).ToArray();

    public static Type[] GetTypes() =>
        windows.Select(x => x.Value).ToArray();

    public static void Create<T>() where T : Window
        => Create(typeof(T));

    public static void Create(Type type)
    {
        if (!IsValidType(type)) return;

        if (Exists(type)) Select(type);
        else Activator.CreateInstance(type);
    }

    public static void Close<T>() where T : Window
        => Close(typeof(T));

    public static void Close(Type type)
    {
        if (!IsValidType(type)) return;

        if (!Exists(type)) return;

        var search = ActiveWindows.Where(x => x.GetType() == type);

        if (!search.Any()) return;

        search.First().Dispose();
    }

    public static void CloseAll()
    {
        while (ActiveWindows.Count > 0)
            ActiveWindows[0].Dispose();
    }

    public static void Select<T>() where T : Window
        => Select(typeof(T));

    public static void Select(Type type)
    {
        if (!IsValidType(type)) return;

        if (!Exists(type)) return;

        ImGui.SetWindowFocus(GetName(type));
    }

    public static bool Exists<T>() where T : Window
        => Exists(typeof(T));

    public static bool Exists(Type type)
    {
        if (!IsValidType(type)) return false;

        foreach (var window in ActiveWindows)
            if (window.GetType() == type)
                return true;

        return false;
    }

    private static bool IsValidType(Type type)
        => type.IsClass && !type.IsAbstract
        && type.IsSubclassOf(typeof(Window));

    private static string SaveStatePath => AppDataPath + "/windows.vdf";

    /// <summary>
    /// Loads saved window state (i.e opened windows) from your app data
    /// </summary>
    internal static void LoadState()
    {
        CloseAll();

        if (!File.Exists(SaveStatePath)) return;

        var serializer = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);

        var stream = File.OpenRead(SaveStatePath);
        var windows = serializer.Deserialize<string[]>(stream);
        stream.Close();

        foreach (var window in windows)
        {
            var type = Type.GetType(window);
            if (type != null) Create(type);
        }
    }

    /// <summary>
    /// Saves the current window state (i.e opened windows) to your app data
    /// </summary>
    internal static void SaveState()
    {
        if (!Directory.Exists(SaveStatePath + "/../"))
            Directory.CreateDirectory(SaveStatePath + "/../");

        var types = ActiveWindows.Select(x => x.GetType()).ToArray();
        var windows = types.Select(x => x.AssemblyQualifiedName);

        var serializer = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);

        var stream = File.Create(SaveStatePath);
        serializer.Serialize(stream, windows, "Windows");
        stream.Close();
    }
}
