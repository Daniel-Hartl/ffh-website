using System.IO;
using System.Windows.Media.Imaging;

namespace FFH_Website_Manager.Classes.Model.Gallery;

public class GalleryImage (BitmapFrame bmp, string source, bool isFromServer = true) : ObservableObject
{
    private BitmapFrame bmp = bmp;
    private string localPath = source;
    private bool isAlreadyUploaded = isFromServer;

    public BitmapFrame Bmp
    {
        get => bmp;
        set
        {
            if (bmp != value)
            {
                bmp = value;
                OnPropChanged();
            }
        }
    }

    public string LocalPath
    {
        get => localPath;
        set
        {
            if (localPath != value)
            {
                localPath = value;
                OnPropChanged();
                OnPropChanged(nameof(FileName));
            }
        }
    }

    public string FileName => Path.GetFileName(localPath);

    public bool IsAlreadyUploaded
    {
        get => isAlreadyUploaded;
        set
        {
            if (isAlreadyUploaded != value)
            {
                isAlreadyUploaded = value;
                OnPropChanged();
            }
        }
    }
}
