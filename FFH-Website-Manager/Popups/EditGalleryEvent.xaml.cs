namespace FFH_Website_Manager.Popups;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.DataProvider;
using FFH_Website_Manager.Classes.Model;
using FFH_Website_Manager.Classes.Model.Gallery;
using FFH_Website_Manager.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

/// <summary>
/// Interaction logic for EditGalleryEvent.xaml
/// </summary>
public partial class EditGalleryEvent : Window, INotifyPropertyChanged, IDisposable
{
    public EditGalleryEvent(GalleryTopic topic, string area)
    {
        this.DataContext = topic;
        this.Topic = topic;
        InitializeComponent();
        LoadBmps();
    }

    private ObservableCollection<BitmapFrame> currentImages = [];
    private ObservableCollection<BitmapFrame> newImages = [];
    private ObservableCollection<BitmapFrame> deletedImages = [];

    public event PropertyChangedEventHandler PropertyChanged;

    internal GalleryTopic Topic { get; set; }
    internal string Area { get; set; }

    public ObservableCollection<BitmapFrame> CurrentImages
    {
        get => currentImages;
        set
        {
            currentImages = value;
            this.OnPropChanged();
        }
    }

    public ObservableCollection<BitmapFrame> NewImages
    {
        get => newImages;
        set
        {
            newImages = value;
            this.OnPropChanged();
        }
    }

    public ObservableCollection<BitmapFrame> DeletedImages
    {
        get => deletedImages;
        set
        {
            deletedImages = value;
            this.OnPropChanged();
        }
    }


    private void OnPropChanged([CallerMemberName] string? src = null) => this.PropertyChanged?.Invoke(this, new(src));

    public void Dispose()
    {
    }

    private void LoadBmps()
    {
        foreach (var img in this.Topic.Inhalt)
        {
            using MemoryStream ms = new();
            App.DataProvider.DownloadFile(GetSftpUrl(img), ms);
            ms.Position = 0;
            var Bmp = BitmapFrame.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            Bmp.Freeze();
            this.CurrentImages.Add(Bmp);
        }
    }

    private string GetSftpUrl(string fileName)
        => App.DataProvider.BuildPath(Appsettings.Instance.RootDirectory,
            PathFragmentCollection.GalleryImageBaseDirectory,
            this.Area,
            this.Topic.Ordner,
            fileName);
}
