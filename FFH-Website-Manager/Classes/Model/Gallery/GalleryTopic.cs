namespace FFH_Website_Manager.Classes.Model.Gallery;

using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

internal class GalleryTopic : GalleryBase
{
    [JsonIgnore]
    private DateTime dateInternal;
    [JsonIgnore]
    private ObservableCollection<string> inhalt;

    [JsonIgnore]
    public DateTime DateInternal
    {
        get => dateInternal;
        set
        {
            dateInternal = value;
            this.OnPropChanged();
        }
    }

    public string Datum
    {
        get => DateInternal.ToString("dd.MM.yyyy");
        set => DateInternal = DateTime.Parse(value);
    }

    public ObservableCollection<string> Inhalt
    {
        get => inhalt;
        set
        {
            inhalt = value;
            this.OnPropChanged();
        }
    }
}
