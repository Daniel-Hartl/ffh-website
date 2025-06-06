namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

internal class BoardViewModel : ViewModelBase
{
    private ObservableCollection<Person> boardMembers;

    public BoardViewModel() : base()
    {
        try
        {
            if (this.sftp is not null)
            {
                string boardStr = sftp.DownloadStringContent(PathFragmentCollection.Board);
                BoardMembers = JsonSerializer.Deserialize<ObservableCollection<Person>>(boardStr);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public ObservableCollection<Person> BoardMembers
    {
        get => boardMembers;
        set
        {
            if (value != boardMembers)
            {
                boardMembers = value;
                this.OnPropChanged();
            }
        }
    }
    public RelayCommand DeleteMemberCommand => new(this.DeleteMember);

    public ObservableCollection<string> Positions { get; set; } =
        ["1. Vorsitzender", "1. Vorsitzende",
        "2. Vorsitzender", "2. Vorsitzende",
        "Ehrenvorsitzender", "Ehrenvorsitzende",
        "1. Schriftführer", "1. Schriftführerin",
        "2. Schriftführer", "2. Schriftführerin",
        "1. Kassier", "2. Kassier",
        "Beisitzer", "Beisitzerin"];

    private void DeleteMember(object obj)
    {
        if (obj is Person person
            && MessageBox.Show($"Wollen Sie wirklich {person.Name} aus der Vorstandschaft entfernen?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            this.BoardMembers.Remove(person);
    }

    private void Save()
    {
        this.sftp.UploadStringContent(PathFragmentCollection.Board, JsonSerializer.Serialize(this.BoardMembers.ToArray()));
    }
