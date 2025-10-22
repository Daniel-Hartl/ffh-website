using System.IO;
using System.Text;
using System.Windows;

namespace FFH_Website_Manager.Classes.DataProvider;
internal class LocalDataProvider : IDataProvider
{
    public void DownloadFile(string path, Stream output, Action<ulong> downloadCallback = null)
    {
        using FileStream fs = File.OpenRead(path);
        output.SetLength(fs.Length);
        fs.Read((output as MemoryStream).GetBuffer(), 0, (int)fs.Length);
    }

    public string DownloadStringContent(string remotePath)
    {
        remotePath = BuildPath(Appsettings.Instance.RootDirectory, remotePath);
        if (!File.Exists(remotePath))
        {
            MessageBox.Show($"Die Konfigurationsdatei \"{remotePath}\" konnte nicht gefunden werden");
            return string.Empty;
        }

        string str = File.ReadAllText(remotePath, Encoding.UTF8);
        return str;
    }

    public void UploadFileFromPath(string path, string remotePath)
    {
        remotePath = Path.Combine(Appsettings.Instance.RootDirectory, remotePath);
        File.Copy(path, remotePath, true);

    }

    public void UploadStringContent(string remotePath, string content)
    {
        remotePath = BuildPath(Appsettings.Instance.RootDirectory, remotePath);
        File.WriteAllText(remotePath, content, Encoding.UTF8);
    }

    public string BuildPath(params string[] paths) => Path.Combine([.. paths.AsEnumerable().Where(x => !string.IsNullOrEmpty(x))]).Replace('/', '\\');

    public void DeleteFile(string path) { /* not needed in local version */ }

    public void RenameFile(string oldPath, string newPath)
    {
        if (File.Exists(oldPath))
            File.Move(oldPath, newPath);
        else if (Directory.Exists(oldPath))
            Directory.Move(oldPath, newPath);
    }

    public void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}