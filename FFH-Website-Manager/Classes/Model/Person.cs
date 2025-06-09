namespace FFH_Website_Manager.Classes.Model;

using Org.BouncyCastle.Tls.Crypto;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media.Imaging;

internal class Person : ObservableObject
{
    #region Backing Fields
    [JsonIgnore]
    private string id;
    [JsonIgnore]
    private ObservableCollection<string> funktion;
    [JsonIgnore]
    private string name;
    [JsonIgnore]
    private string bild;
    [JsonIgnore]
    private string kommentar;
    [JsonIgnore]
    private BitmapFrame wpfImage;
    #endregion

    #region Export Properties
    public string Id
    {
        get => id;
        set
        {
            id = value;
            this.OnPropChanged();
        }
    }
    public ObservableCollection<string> Funktion
    {
        get => funktion;
        set
        {
            funktion = value;
            this.OnPropChanged();
            this.AdjustId();
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
    public string Kommentar
    {
        get => kommentar;
        set
        {
            kommentar = value;
            this.OnPropChanged();
        }
    }
    #endregion

    #region WPF Properties
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
    public BitmapFrame WpfImage
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
                catch (Exception exc)
                {
                    MessageBox.Show(
                        $"Fehler beim Laden des Bildes {this.Bild}. Möglicherweise muss neu hochgeladen werden.",
                        "Fehler beim Laden eines Bildes",
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
            this.OnPropChanged(nameof(HasImage));
        }
    }

    [JsonIgnore]
    public string UploadImagePath { get; set; }

    [JsonIgnore]
    public bool DeleteImage { get; set; }

    [JsonIgnore]
    public bool HasImage => !DeleteImage && (!string.IsNullOrEmpty(this.Bild) || !string.IsNullOrEmpty(this.UploadImagePath));

    [JsonIgnore]
    public string Func1
    {
        get => this.Funktion.ElementAtOrDefault(0) ?? string.Empty;
        set => this.SetFunctions(0, value);
    }

    [JsonIgnore]
    public string Func2
    {
        get => this.Funktion.ElementAtOrDefault(1) ?? string.Empty;
        set => this.SetFunctions(1, value);
    }

    [JsonIgnore]
    public string Func3
    {
        get => this.Funktion.ElementAtOrDefault(2) ?? string.Empty;
        set => this.SetFunctions(2, value);
    }

    [JsonIgnore]
    public string Func4
    {
        get => this.Funktion.ElementAtOrDefault(3) ?? string.Empty;
        set => this.SetFunctions(3, value);
    }
    #endregion

    private void SetFunctions(int index, string value)
    {
        if (index < this.Funktion.Count)
        {
            this.Funktion[index] = value;
        }
        else
        {
            this.Funktion.Add(value);
        }

        this.OnPropChanged(nameof(Func1));
        this.OnPropChanged(nameof(Func2));
        this.OnPropChanged(nameof(Func3));
        this.OnPropChanged(nameof(Func4));
    }

    private void AdjustId()
    {
        this.Id = this.Funktion?[0] switch
        {
            // aktive Mannschaft
            "1. Kommandantin" => "kommandant",
            "1. Kommandant" => "kommandant",
            "Stellv. Kommandant" => "kommandant",
            "Stellv. Kommandantin" => "kommandant",
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
            "Datenschutzbeauftragter" => "amt",
            "Datenschutzbeauftragte" => "amt",
            _ => "sonstige"
        };
    }
}
