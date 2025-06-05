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
                string boardStr = sftp.DownloadStringContent("test/board.json");
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
}
