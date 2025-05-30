using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace FFH_Website_Manager.Classes.Model;

internal class Article: ObservableObject
{
    [JsonIgnore]
    private string titel;
    [JsonIgnore]
    private DateTime dateInternal;
    [JsonIgnore]
    private string autor;
    [JsonIgnore]
    private string bild;
    [JsonIgnore]
    private string bildquelle;
    [JsonIgnore]
    private string inhalt;

    public string Titel
    {
        get => titel;
        set
        {
            titel = value;
            this.OnPropChanged();
        }
    }

    public string Datum
    {
        get => DateInternal.ToString("dd.MM.yyyy");
        set => DateInternal = DateTime.Parse(value);
    }

    public string Autor
    {
        get => autor;
        set
        {
            autor = value;
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
    public string Bildquelle
    {
        get => bildquelle;
        set
        {
            bildquelle = value;
            this.OnPropChanged();
        }
    }

    public string Inhalt
    {
        get => inhalt;
        set
        {
            inhalt = value;
            this.OnPropChanged();
        }
    }

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
}
