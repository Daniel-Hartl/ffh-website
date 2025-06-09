namespace FFH_Website_Manager.Classes.Model;

using System.Text.Json.Serialization;

internal class Statistic : ObservableObject
{
    [JsonIgnore]
    private int mitglieder;
    [JsonIgnore]
    private int aktive;
    [JsonIgnore]
    private int einsätze;

    public int Mitglieder
    {
        get => mitglieder;
        set
        {
            mitglieder = value;
            this.OnPropChanged();
        }
    }

    public int Aktive
    {
        get => aktive;
        set
        {
            aktive = value;
            this.OnPropChanged();
        }
    }

    public int Einsätze
    {
        get => einsätze;
        set
        {
            einsätze = value;
            this.OnPropChanged();
        }
    }
}
