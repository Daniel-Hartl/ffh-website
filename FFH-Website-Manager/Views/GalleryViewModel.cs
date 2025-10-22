namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.DataProvider;
using FFH_Website_Manager.Classes.Model.Gallery;
using FFH_Website_Manager.Popups;
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
    public RelayCommand DeleteEventCommand => new(DeleteEvent);

    public bool IsActiveSelected { get; set; }

    protected override void LoadData(object obj)
    {
        try
        {
            if (this.sftp is not null)
            {
                string galleryStr = sftp.DownloadStringContent(PathFragmentCollection.Gallery);
                GalleryAreas = JsonSerializer.Deserialize<ObservableCollection<GalleryArea>>(galleryStr);
                foreach (var area in GalleryAreas)
                {
                    area.Inhalt = [.. area.Inhalt.OrderByDescending(x => x.DateInternal)];

                    foreach (var folder in area.Inhalt)
                        folder.Parent = area;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void EditEvent(object obj)
    {
        if (obj is GalleryTopic tp)
        {
            var dlg = new EditGalleryEvent(tp, tp.Parent.Ordner);
            dlg.ShowDialog();
            if (dlg.Save)
            {

                this.sftp.UploadStringContent(PathFragmentCollection.Gallery, JsonSerializer.Serialize(this.GalleryAreas, App.SerializerConfig));
            }
        }
    }

    private void AddEvent(object obj)
    {
        using GalleryAreaSelector gas = new(IsActiveSelected);
        gas.ShowDialog();
        if (!gas.Succeed)
            return;

        GalleryTopic tp = new()
        {
            Parent = gas.IsActiveSelected ? this.galleryAreas.First(x => x.Ordner == "aktiv") : this.galleryAreas.First(x => x.Ordner == "verein"),
            DateInternal = DateTime.Today
        };
        using var dlg = new EditGalleryEvent(tp, tp.Parent.Ordner, true);
        dlg.ShowDialog();
        if (dlg.Save)
        {
            this.sftp.UploadStringContent(PathFragmentCollection.Gallery, JsonSerializer.Serialize(this.GalleryAreas, App.SerializerConfig));
        }
    }

    private void DeleteEvent(object obj)
    {
        if (obj is GalleryTopic tp)
        {
            if (MessageBox.Show($"Wollen Sie das Event {tp.Ordner} und die dazu hinterlegten Bilder wirklich endgültig löschen?",
                string.Empty,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                foreach(string image in tp.Inhalt)
                {
                    this.sftp.DeleteFile(this.sftp.BuildPath(Appsettings.Instance.RootDirectory,
                        PathFragmentCollection.GalleryImageBaseDirectory,
                        tp.Parent.Ordner,
                        tp.Ordner,
                        image));
                }

                tp.Parent.Inhalt.Remove(tp);
                this.sftp.UploadStringContent(PathFragmentCollection.Gallery, JsonSerializer.Serialize(this.GalleryAreas, App.SerializerConfig));
            }
        }
    }
}
