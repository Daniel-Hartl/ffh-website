namespace FFH_Website_Manager.Classes.Model.Gallery;

using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

internal class GalleryArea : GalleryBase
{
    [JsonIgnore]
    private ObservableCollection<GalleryTopic> inhalt;

    public ObservableCollection<GalleryTopic> Inhalt
    {
        get => inhalt;
        set
        {
            inhalt = value;
            this.OnPropChanged();
        }
    }
}
