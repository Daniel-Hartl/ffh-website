namespace FFH_Website_Manager.Classes.Model.Gallery;

using System.Text.Json.Serialization;

internal class GalleryTopic : GalleryArea
{
    [JsonIgnore]
    private DateTime dateInternal;

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
}
