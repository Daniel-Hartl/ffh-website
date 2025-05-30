namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model.Gallery;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

internal class GalleryViewModel : ViewModelBase
{
    private ObservableCollection<GalleryArea> galleryAreas;

    public GalleryViewModel() : base()
    {
        try
        {
            string galleryStr = sftp.DownloadStringContent("test/gallery.json");
            GalleryAreas = JsonSerializer.Deserialize<ObservableCollection<GalleryArea>>(galleryStr);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public ObservableCollection<GalleryArea> GalleryAreas
    {
        get => galleryAreas;
        set
        {
            if (value != galleryAreas)
            {
                galleryAreas = value;
                this.OnPropChanged();
            }
        }
    }
}
