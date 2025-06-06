namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

internal class CrewViewModel : ViewModelBase
{
    private ObservableCollection<Person> crewMembers;

    public CrewViewModel() : base()
    {
        try
        {
            if (this.sftp is not null)
            {
                string crewStr = sftp.DownloadStringContent("test/crew.json");
                CrewMembers = JsonSerializer.Deserialize<ObservableCollection<Person>>(crewStr);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public ObservableCollection<Person> CrewMembers
    {
        get => crewMembers;
        set
        {
            if (value != crewMembers)
            {
                crewMembers = value;
                this.OnPropChanged();
            }
        }
    }



    public ObservableCollection<string> Positions { get; set; } =
        ["1. Kommandant", "1. Kommandantin", 
        "stellv. Kommandant", "stellv. Kommandantin",
        "Ehrenkommandant", "Ehrenkommandantin",
        "Hauptlöschmeister", "Hauptlöschmeisterin",
        "Oberlöschmeister", "Oberlöschmeisterin",
        "Löschmeister", "Löschmeisterin",
        "1. Gerätewart", "2. Gerätewart",
        "1. Jugendbeauftragter", "1. Jugendbeauftragte",
        "2. Jugendbeauftragter", "2. Jugendbeauftragte"];
}
