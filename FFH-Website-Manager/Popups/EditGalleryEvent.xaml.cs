namespace FFH_Website_Manager.Popups;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.DataProvider;
using FFH_Website_Manager.Classes.Model.Gallery;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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

    public event PropertyChangedEventHandler PropertyChanged;

    internal GalleryTopic Topic { get; set; }
    internal string Area { get; set; }


    private void OnPropChanged([CallerMemberName] string? src = null) => this.PropertyChanged?.Invoke(this, new(src));

    public void Dispose()
    {
    }

    private void LoadBmps()
    {
        this.Topic.Content.Clear();
        this.Topic.DeletedRemote.Clear();
        foreach (var img in this.Topic.Inhalt)
        {
            using MemoryStream ms = new();
            App.DataProvider.DownloadFile(GetSftpUrl(img), ms);
            ms.Position = 0;
            var bmp = BitmapFrame.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            bmp.Freeze();
            this.Topic.Content.Add(new(bmp, string.Empty));
        }
    }

    private string GetSftpUrl(string fileName)
        => App.DataProvider.BuildPath(Appsettings.Instance.RootDirectory,
            PathFragmentCollection.GalleryImageBaseDirectory,
            this.Area,
            this.Topic.Ordner,
            fileName);

    private void DeleteImage(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is GalleryImage img)
        {
            this.Topic.Content.Remove(img);
            if (img.IsAlreadyUploaded)
                this.Topic.DeletedRemote.Add(img);
        }
    }

    private void ReuseImage(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is GalleryImage img)
        {
            this.Topic.DeletedRemote.Remove(img);
            this.Topic.Content.Add(img);
        }
    }

    private void AddImage(object sender, RoutedEventArgs e)
    {
        if (FileDialogTemplates.SelectMultipleImages(out List<string> paths))
        {
            foreach (var image in paths)
            {
                var bmp = BitmapFrame.Create(new Uri(image));
                bmp.Freeze();
                this.Topic.Content.Add(new GalleryImage(bmp, image, false));
            }
        }
    }

    private void SaveTopic(object sender, RoutedEventArgs e)
    {
        foreach (var img in this.Topic.Content)
        {
            if (!img.IsAlreadyUploaded)
            {
                App.DataProvider.UploadFileFromPath(img.LocalPath, GetSftpUrl(img.FileName));
                this.Topic.Inhalt.Add(img.FileName);
            }
        }

        foreach(var img in this.Topic.DeletedRemote)
            App.DataProvider.DeleteFile(GetSftpUrl(img.FileName));
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Reset(object sender, RoutedEventArgs e)
    {
        this.LoadBmps();
    }
}
