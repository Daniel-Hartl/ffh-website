namespace FFH_Website_Manager.Classes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

internal static class Appsettings
{
    private static readonly IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    internal static string Host => config.GetSection("Credentials")["Host"];
    internal static string User => config.GetSection("Credentials")["User"];
    internal static string Password => config.GetSection("Credentials")["Password"];
    internal static string RootDirectory => config.GetSection("Credentials")["RootDirectory"];
}
