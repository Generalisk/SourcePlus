using System.Reflection;
using System.Xml.Linq;

namespace SourcePlus.Editor.Windows;

internal static class WindowHandler
{
    private static KeyValuePair<string, Type>[] windows = { };

    internal static void Init()
    {
        // Retrieve window menu buttons
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Window)));

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

    public static string? GetWindowName<T>() => GetWindowName(typeof(T));

    public static string? GetWindowName(Type type)
    {
        var search = windows.Where(x => x.Value == type);

        if (search.Any())
            return search.First().Key;
        else
            return null;
    }

    public static Type? GetWindowType(string name)
    {
        var search = windows.Where(x => x.Key.ToLower() == name.ToLower());

        if (search.Any())
            return search.First().Value;
        else
            return null;
    }

    public static string[] GetWindowNames() =>
        windows.Select(x => x.Key).ToArray();

    public static Type[] GetWindowTypes() =>
        windows.Select(x => x.Value).ToArray();

    public static void Create<T>() where T : Window
        => Create(typeof(T));

    public static void Create(Type type)
        => Activator.CreateInstance(type);

    public static bool Exists<T>() where T : Window
        => Exists(typeof(T));

    public static bool Exists(Type type)
    {
        foreach (var window in ActiveWindows)
            if (window.GetType() == type)
                return true;

        return false;
    }
}
