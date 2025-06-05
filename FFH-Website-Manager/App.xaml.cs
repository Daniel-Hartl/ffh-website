using FFH_Website_Manager.Classes;
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
            SFTPProvider = new();
            SFTPProvider.Connect();
            this.Exit += this.OnExit;
        }

        internal static SFTPProvider SFTPProvider { get; set; }

        private void OnExit(object sender, ExitEventArgs e)
        {
            this.Exit -= this.OnExit;
            if (SFTPProvider.IsConnected)
                SFTPProvider?.Disconnect();
        }
    }

}
