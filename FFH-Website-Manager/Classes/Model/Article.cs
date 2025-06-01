namespace FFH_Website_Manager.Classes.Model;

using System.Text.Json.Serialization;
internal class Article : ObservableObject
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
            this.OnPropChanged(nameof(this.HasImange));
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

    [JsonIgnore]
    public bool HasImange => !string.IsNullOrEmpty(this.Bild);

    public Article Copy()
        => new Article
        {
            Titel = this.Titel,
            DateInternal = this.DateInternal,
            Autor = this.Autor,
            Bild = this.Bild,
            Bildquelle = this.Bildquelle,
            Inhalt = this.Inhalt,
        };

    public void Insert(Article article)
    {
        this.Titel = article.Titel;
        this.DateInternal = article.DateInternal;
        this.Autor = article.Autor;
        this.Bild = article.Bild;
        this.Bildquelle = article.Bildquelle;
        this.Inhalt = article.Inhalt;
    }
}
