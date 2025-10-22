namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.DataProvider;
using FFH_Website_Manager.Classes.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

internal class AppointmentViewModel : ViewModelBase
{
    private ObservableCollection<Appointment> appointments;
    public AppointmentViewModel() : base()
    {
        this.LoadData(null);
    }
    public RelayCommand DeleteAppointmentCommand => new(this.DeleteAppointment);

    public ObservableCollection<Appointment> Appointments
    {
        get => this.appointments;
        set
        {
            if (value != appointments)
            {
                appointments.ToList().ForEach(x => x.PropertyChanged -= this.StateChanging);
                appointments = value;
                appointments.ToList().ForEach(x => x.PropertyChanged += this.StateChanging);
                this.OnPropChanged();
            }
        }
    }

    public ObservableCollection<string> Roles { get; set; } =
        [string.Empty, "Verein", "Aktive", "Jugend"];

    protected override void LoadData(object obj)
    {
        this.StateHasChanged = false;
        try
        {
            if (this.sftp is not null)
            {
                string appsStr = sftp.DownloadStringContent(PathFragmentCollection.Appointments);
                appointments = [..JsonSerializer.Deserialize<ObservableCollection<Appointment>>(appsStr).OrderBy(x => x.DateInternal)];
                if (appointments.Any(x => x.DateInternal < DateTime.Now) &&
                    MessageBox.Show("In der Liste sind vergangene Termine. Sollen diese automatisch gelöscht werden?", "Vergangene Termine!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    appointments = [.. appointments.Where(x => x.DateInternal > DateTime.Now)];
                    this.Save(null);
                }

                appointments.ToList().ForEach(x => x.PropertyChanged += this.StateChanging);
                this.OnPropChanged(nameof(Appointments));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override void Save(object obj)
    {
        this.sftp.UploadStringContent(PathFragmentCollection.Appointments, JsonSerializer.Serialize(this.Appointments.OrderBy(x => x.DateInternal), App.SerializerConfig));
        this.StateHasChanged = false;
    }

    private void DeleteAppointment(object appointment)
    {
        if (appointment is Appointment app)
        {
            this.Appointments.Remove(app);
            this.StateHasChanged = true;
        }
    }
}
