namespace FFH_Website_Manager.Classes.DataProvider;

using Renci.SshNet;
using System.IO;
using System.Text;
using System.Windows;

internal class SFTPDataProvider : SftpClient, IDataProvider
{
    public SFTPDataProvider()
        : base(Appsettings.Instance.Host, 22, Appsettings.Instance.User, Appsettings.Instance.Password)
    {

    }

    public string DownloadStringContent(string remotePath)
    {
        remotePath = BuildPath(Appsettings.Instance.RootDirectory, remotePath);

        try
        {
            using MemoryStream ms = new();
            this.DownloadFile(remotePath, ms);
            ms.Position = 0;
            using StreamReader sr = new(ms);
            string str = sr.ReadToEnd();
            return str;
        }
        catch (Exception ex)
        {
            string uploadFile = remotePath[remotePath.LastIndexOf("/")..];
            string errorString = $"Die Konfigurationsdatei {uploadFile} konnte nicht heruntergeladen werden. Grund: {ex.Message}";
            MessageBox.Show(errorString, "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            return string.Empty;
        }
    }

    public void UploadFileFromPath(string path, string remotePath)
    {
        using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
        
        try
        {
            this.UploadFile(fs, remotePath);
        }
        catch (Exception ex)
        {
            string uploadFile = Path.GetFileName(path);
            string errorString = $"Die Datei {uploadFile} konnte nicht hochgeladen werden. Grund: {ex.Message}";
            MessageBox.Show(errorString, "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    public void UploadStringContent(string remotePath, string content)
    {
        remotePath = BuildPath(Appsettings.Instance.RootDirectory, remotePath);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(content));
        ms.Position = 0;
        try
        {
            this.UploadFile(ms, remotePath);
        }
        catch (Exception ex)
        {
            string uploadFile = remotePath[remotePath.LastIndexOf("/")..];
            string errorString = $"Die Konfigurationsdatei {uploadFile} konnte nicht hochgeladen werden. Grund: {ex.Message}";
            MessageBox.Show(errorString, "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    public string BuildPath(params string[] paths) => string.Join("/", paths);
}