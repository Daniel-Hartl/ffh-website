using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model.Gallery;
using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Windows;

namespace FFH_Website_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        { 
            SFTPProvider = new ();
            SFTPProvider.Connect();
        }
        internal static SFTPProvider SFTPProvider { get; set; }
    }

}
