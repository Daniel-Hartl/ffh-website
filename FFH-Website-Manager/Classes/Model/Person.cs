namespace FFH_Website_Manager.Classes.Model;

using System.Text.Json.Serialization;

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
}
