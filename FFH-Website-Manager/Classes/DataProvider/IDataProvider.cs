using System.IO;

namespace FFH_Website_Manager.Classes.DataProvider;
internal interface IDataProvider
{
    public string DownloadStringContent(string remotePath);

    public void UploadFileFromPath(string path, string remotePath);

    public void UploadStringContent(string remotePath, string content);

    public void DownloadFile(string path, Stream output, Action<ulong>? downloadCallback = null);

    public void DeleteFile(string path);

    public void RenameFile(string oldPath, string newPath);

    public string BuildPath(params string[] paths);
}
