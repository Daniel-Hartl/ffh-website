namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using System.Text.Json;
using System.Windows;

internal class StatisticsViewModel : ViewModelBase
{
    private Statistic statistic;

    public StatisticsViewModel() : base()
    {
        this.LoadData(null);
    }

    public Statistic Statistic
    {
        get => this.statistic;
        set
        {
            if (this.statistic is not null)
                this.statistic.PropertyChanged -= this.StateChanging;
            this.statistic = value;
            this.statistic.PropertyChanged += this.StateChanging;
            this.OnPropChanged();
        }
    }

    protected override void LoadData(object obj)
    {
        this.StateHasChanged = false;
        try
        {
            if (this.sftp is not null)
            {
                string statisticStr = sftp.DownloadStringContent("test/statistics.json");
                Statistic = JsonSerializer.Deserialize<Statistic>(statisticStr);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override void Save(object obj)
    {
        this.sftp.UploadStringContent(PathFragmentCollection.Statistics, JsonSerializer.Serialize(this.Statistic));
        this.StateHasChanged = false;
    }
}
