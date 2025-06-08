namespace FFH_Website_Manager.Classes;

using Renci.SshNet;
using System.IO;
using System.Text;

internal class SFTPProvider : SftpClient
{
    public SFTPProvider()
        : base(Appsettings.Instance.Host, 22, Appsettings.Instance.User, Appsettings.Instance.Password)
    {

    }

    public string DownloadStringContent(string remotePath)
    {
        remotePath = BuildPath(Appsettings.Instance.RootDirectory, remotePath);

        using MemoryStream ms = new();
        this.DownloadFile(remotePath, ms);
        ms.Position = 0;
        using StreamReader sr = new(ms);
        string str = sr.ReadToEnd();
        return str;
    }

    public void UploadFileFromPath(string path, string remotePath)
    {
        using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
        this.UploadFile(fs, remotePath);
    }

    public void UploadStringContent(string remotePath, string content)
    {
        remotePath = Appsettings.Instance.RootDirectory + "/" + remotePath;

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(content));
        ms.Position = 0;

        this.UploadFile(ms, remotePath);
    }

    public static string BuildPath(params string[] paths) => string.Join("/", paths);
}