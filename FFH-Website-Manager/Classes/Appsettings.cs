namespace FFH_Website_Manager.Classes;

using System.IO;
using System.Text.Json;

internal class Appsettings
{
    internal static Appsettings Instance;
    static Appsettings()
    {
        string str = File.ReadAllText("appsettings.json");
        Instance = JsonSerializer.Deserialize<Appsettings>(str)!;
    }

    public string Host { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string RootDirectory { get; set; }
    public bool LocalMode { get; set; }
}
