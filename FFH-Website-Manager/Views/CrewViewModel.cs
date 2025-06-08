namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using System.Collections.ObjectModel;

internal class CrewViewModel : PersonsViewModelBase
{    
    protected override string JsonPath { get; } = PathFragmentCollection.Crew;

    public override ObservableCollection<string> Positions { get; set; } =
        ["1. Kommandant", "1. Kommandantin", 
        "Stellv. Kommandant", "Stellv. Kommandantin",
        "Ehrenkommandant", "Ehrenkommandantin",
        "Hauptlöschmeister", "Hauptlöschmeisterin",
        "Oberlöschmeister", "Oberlöschmeisterin",
        "Löschmeister", "Löschmeisterin",
        "1. Gerätewart", "2. Gerätewart",
        "1. Jugendbeauftragter", "1. Jugendbeauftragte",
        "2. Jugendbeauftragter", "2. Jugendbeauftragte"];
}
