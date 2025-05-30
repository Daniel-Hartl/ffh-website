namespace FFH_Website_Manager.Classes.Model.Gallery;

using System.Text.Json.Serialization;

internal abstract class GalleryBase : ObservableObject
{
    [JsonIgnore]
    private string ordner;

    public string Ordner
    {
        get => ordner;
        set
        {
            ordner = value;
            this.OnPropChanged();
        }
    }
}
