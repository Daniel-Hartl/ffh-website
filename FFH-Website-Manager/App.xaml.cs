using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.DataProvider;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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
            SerializerConfig = new()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };

            if (Appsettings.Instance.LocalMode)
            {
                DataProvider = new LocalDataProvider();
            }
            else
            {
                var provider = new SFTPDataProvider();
                provider.Connect();
                DataProvider = provider;
                this.Exit += this.OnExit;
            }
        }

        internal static IDataProvider DataProvider { get; set; }
        internal static JsonSerializerOptions SerializerConfig { get; set; }
        private void OnExit(object sender, ExitEventArgs e)
        {
            this.Exit -= this.OnExit;
            if (DataProvider is SFTPDataProvider dp && dp.IsConnected)
                dp?.Disconnect();
        }
    }

}
