namespace FFH_Website_Manager.Classes;

using Renci.SshNet;
using System.IO;

internal class SFTPProvider : SftpClient
{
    public SFTPProvider()
        : base(Appsettings.Instance.Host, 22, Appsettings.Instance.User, Appsettings.Instance.Password)
    {

    }

    public string DownloadStringContent(string remotePath)
    {
        remotePath = Appsettings.Instance.RootDirectory + "/" + remotePath;

        using MemoryStream ms = new ();
        this.DownloadFile(remotePath, ms);
        ms.Position = 0;
        using StreamReader sr = new StreamReader(ms);
        string str = sr.ReadToEnd();
        return str;
    }
}