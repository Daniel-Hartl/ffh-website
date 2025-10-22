namespace FFH_Website_Manager.Popups;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.DataProvider;
using FFH_Website_Manager.Classes.Model.Gallery;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

/// <summary>
/// Interaction logic for EditGalleryEvent.xaml
/// </summary>
public partial class EditGalleryEvent : Window, INotifyPropertyChanged, IDisposable
{
    private string oldDirectory;

    public EditGalleryEvent(GalleryTopic topic, string area, bool isNewEvent = false)
    {
        this.DataContext = topic;
        this.Topic = topic;
        this.oldDirectory = topic.Ordner;
        this.Area = area;
        this.IsNewEvent = isNewEvent;
        InitializeComponent();
        if (isNewEvent)
            this.resetBtn.IsEnabled = false;
        LoadBmps();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    internal GalleryTopic Topic { get; init; }
    internal string Area { get; init; }
    internal bool Save { get; private set; }
    internal bool IsNewEvent { get; init; }

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
            this.Topic.Content.Add(new(bmp, img));
        }
    }

    private string GetSftpUrl(string fileName, bool useNewName = true)
        => App.DataProvider.BuildPath(Appsettings.Instance.RootDirectory,
            PathFragmentCollection.GalleryImageBaseDirectory,
            this.Area,
            useNewName ? this.Topic.Ordner : this.oldDirectory,
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
        if (this.oldDirectory != this.Topic.Ordner && !this.IsNewEvent)
            App.DataProvider.RenameFile(GetSftpUrl(string.Empty, false), GetSftpUrl(string.Empty));

        if(this.IsNewEvent)
        {
            App.DataProvider.EnsureDirectoryExists(GetSftpUrl(string.Empty));
            this.Topic.Parent.Inhalt.Add(this.Topic);
        }

        foreach (var img in this.Topic.Content)
        {
            if (!img.IsAlreadyUploaded)
            {
                App.DataProvider.UploadFileFromPath(img.LocalPath, GetSftpUrl(img.FileName));
                this.Topic.Inhalt.Add(img.FileName);
            }
        }

        foreach (var img in this.Topic.DeletedRemote.Select(x => x.FileName))
        {
            App.DataProvider.DeleteFile(GetSftpUrl(img));
            this.Topic.Inhalt.Remove(img);

        }

        this.Save = true;
        this.Close();
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
