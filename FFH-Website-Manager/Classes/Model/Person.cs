namespace FFH_Website_Manager.Classes.Model;

using FFH_Website_Manager.Views;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media.Imaging;

internal class Person : ObservableObject
{
    [JsonIgnore]
    private string id;
    [JsonIgnore]
    private string funktion;
    [JsonIgnore]
    private string name;
    [JsonIgnore]
    private string bild;
    [JsonIgnore]
    private BitmapFrame wpfImage;

    public string Id
    {
        get => id;
        set
        {
            id = value;
            this.OnPropChanged();
        }
    }
    public string Funktion
    {
        get => funktion;
        set
        {
            funktion = value;
            this.OnPropChanged();
            this.Id = this.Funktion switch
            {
                // aktive Mannschaft
                "1. Kommandantin" => "kommandant",
                "1. Kommandant" => "kommandant",
                "stellv. Kommandant" => "kommandant",
                "stellv. Kommandantin" => "kommandant",
                "Ehrenkommandant" => "kommandant",
                "Ehrenkommandantin" => "kommandant",
                "Hauptlöschmeister" => "löschmeister",
                "Hauptlöschmeisterin" => "löschmeister",
                "Oberlöschmeister" => "löschmeister",
                "Oberlöschmeisterin" => "löschmeister",
                "Löschmeister" => "löschmeister",
                "Löschmeisterin" => "löschmeister",
                "1. Gerätewart" => "amt",
                "2. Gerätewart" => "amt",
                "1. Jugendbeauftragter" => "amt",
                "1. Jugendbeauftragte" => "amt",
                "2. Jugendbeauftragter" => "amt",
                "2. Jugendbeauftragte" => "amt",
                // Verein
                "1. Vorsitzender" => "vorsitz",
                "1. Vorsitzende" => "vorsitz",
                "2. Vorsitzender" => "vorsitz",
                "2. Vorsitzende" => "vorsitz",
                "Ehrenvorsitzender" => "vorsitz",
                "Ehrenvorsitzende" => "vorsitz",
                "1. Schriftführer" => "amt",
                "1. Schriftführerin" => "amt",
                "2. Schriftführer" => "amt",
                "2. Schriftführerin" => "amt",
                "1. Kassier" => "amt",
                "2. Kassier" => "amt",
                "Beisitzer" => "beisitz",
                "Beisitzerin" => "beisitz",
                _ => this.Id
            };
        }
    }
    public string Name
    {
        get => name;
        set
        {
            name = value;
            this.OnPropChanged();
        }
    }
    public string Bild
    {
        get => bild;
        set
        {
            bild = value;
            this.OnPropChanged();
        }
    }

    [JsonIgnore]
    public string DisplayId
        => this.Id switch
        {
            "vorsitz" => "Vorsitzende",
            "kommandant" => "Kommandanten",
            "amt" => "Funktionäre",
            "beisitz" => "Beisitzer",
            "löschmeister" => "Führungsränge",
            _ => "Sonstige"
        };

    [JsonIgnore]
    public BitmapFrame? WpfImage
    {
        get
        {
            if (this.DeleteImage)
                return null;
            if (this.wpfImage is null)
            {
                if (string.IsNullOrEmpty(this.Bild))
                    return null;
                try
                {
                    using MemoryStream ms = new();
                    App.SFTPProvider.DownloadFile(SFTPProvider.BuildPath(
                        Appsettings.Instance.RootDirectory,
                        PathFragmentCollection.PersonImageDirectory,
                        this.Bild), ms);
                    ms.Position = 0;
                    this.wpfImage = BitmapFrame.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    this.wpfImage.Freeze();
                }
                catch(Exception exc)
                {
                    MessageBox.Show(
                        "Das Bild muss neu hochgeladen werden.",
                        "Fehler beim Laden des Bildes",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            return this.wpfImage;
        }
        set
        {
            if (value == this.wpfImage) return;
            this.wpfImage = value;
            this.OnPropChanged();
        }
    }

    [JsonIgnore]
    public string UploadImagePath { get; set; }

    [JsonIgnore]
    public bool DeleteImage { get; set; }
}
