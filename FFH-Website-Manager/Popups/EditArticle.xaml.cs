namespace FFH_Website_Manager.Popups;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using Microsoft.Win32;
using Org.BouncyCastle.Tls.Crypto;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

/// <summary>
/// Interaction logic for EditArticle.xaml
/// </summary>
public partial class EditArticle : Window, IDisposable, INotifyPropertyChanged
{
    private string uploadImagePath;
    private bool imageHasChanged;
    private bool oldImageCorrupted;
    public event PropertyChangedEventHandler? PropertyChanged;

    internal EditArticle(Article article)
    {
        this.DataContext = article;
        this.Article = article;
        if (this.Article.HasImange)
        {
            try
            {
                using MemoryStream ms = new();
                App.SFTPProvider.DownloadFile(GetSftpUrl(article.Bild), ms);
                ms.Position = 0;
                Bmp = BitmapFrame.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                Bmp.Freeze();
            }
            catch
            {
                oldImageCorrupted = true;
                MessageBox.Show(
                    "Das Bild muss neu hochgeladen werden.",
                    "Fehler beim Laden des Bildes",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        InitializeComponent();
        this.OnPropChanged(nameof(Bmp));
    }

    public BitmapFrame Bmp { get; set; }

    internal Article Article { get; set; }

    public bool SaveData { get; set; }

    private static string GetSftpUrl(string fileName) => Appsettings.Instance.RootDirectory + "/test/" + fileName;

    private void AddPicture(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new();
        ofd.Multiselect = false;
        ofd.Title = "Bild auswählen...";
        ofd.Filter = "Bilder (*.jpg, *.png, *.jpeg)|*.jpg; *.png; *.jpeg";
        if (ofd.ShowDialog() ?? false)
        {
            uploadImagePath = ofd.FileName;
            Bmp = BitmapFrame.Create(new Uri(uploadImagePath));
            this.imageHasChanged = true;
            this.OnPropChanged(nameof(Bmp));
        }
    }

    private void RemovePicture(object sender, RoutedEventArgs e)
    {
        Bmp = null;
        this.imageHasChanged = true;
        this.OnPropChanged(nameof(Bmp));
    }

    private void Save(object sender, RoutedEventArgs e)
    {
        this.SaveData = true;
        if (this.imageHasChanged && !this.oldImageCorrupted && !string.IsNullOrEmpty(this.Article.Bild))
            App.SFTPProvider.DeleteFile(GetSftpUrl(this.Article.Bild));

        if (Bmp == null)
        {
            this.Article.Bild = string.Empty;
        }
        else if (!string.IsNullOrEmpty(this.uploadImagePath))
        {
            this.Article.Bild = this.Article.Titel + Path.GetExtension(this.uploadImagePath);
            App.SFTPProvider.UploadFileFromPath(this.uploadImagePath, GetSftpUrl(this.Article.Bild));
        }

        this.Close();
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public void Dispose()
    {
        this.Close();
    }
    private void OnPropChanged([CallerMemberName] string? src = null) => this.PropertyChanged?.Invoke(this, new(src));
}
