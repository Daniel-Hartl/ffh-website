using System.Text.Json.Serialization;

namespace FFH_Website_Manager.Classes.Model.Gallery;


internal class GalleryArea : ObservableObject
{
    [JsonIgnore]
    private string ordner;

    [JsonIgnore]
    private List<string> inhalt;

    public string Ordner
    {
        get => ordner;
        set
        {
            ordner = value;
            this.OnPropChanged();
        }
    }

    public List<string> Inhalt
    {
        get => inhalt;
        set
        {
            inhalt = value;
            this.OnPropChanged();
        }
    }
}
