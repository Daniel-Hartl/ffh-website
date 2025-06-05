namespace FFH_Website_Manager.Classes;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
}
