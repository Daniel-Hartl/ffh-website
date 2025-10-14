namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.DataProvider;
using FFH_Website_Manager.Classes.Model;
using FFH_Website_Manager.Classes.Model.Gallery;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

internal class GalleryViewModel : ViewModelBase
{
    private ObservableCollection<GalleryArea> galleryAreas;

    public GalleryViewModel() : base() => this.LoadData(null);

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

    public RelayCommand EditEventCommand => new(EditEvent);
    public RelayCommand AddEventCommand => new(AddEvent);

    protected override void LoadData(object obj)
    {
        try
        {
            if (this.sftp is not null)
            {
                string galleryStr = sftp.DownloadStringContent(PathFragmentCollection.Gallery);
                GalleryAreas = JsonSerializer.Deserialize<ObservableCollection<GalleryArea>>(galleryStr);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void EditEvent(object obj)
    {
    }

    private void AddEvent(object obj)
    {
    }
}
