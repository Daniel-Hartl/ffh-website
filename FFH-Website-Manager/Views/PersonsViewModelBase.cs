namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;
using System.ComponentModel;

internal abstract class PersonsViewModelBase : ViewModelBase
{
    private ObservableCollection<Person> boardMembers;
    private bool stateHasChanged;
    protected virtual string JsonPath { get; }

    protected PersonsViewModelBase() : base()
    {
        this.LoadData(null);
    }

    public ObservableCollection<Person> BoardMembers
    {
        get => boardMembers;
        set
        {
            if (value != boardMembers)
            {
                boardMembers.ToList().ForEach(x => x.PropertyChanged -= this.OnValueChanged);
                boardMembers = value;
                boardMembers.ToList().ForEach(x => x.PropertyChanged += this.OnValueChanged);
                this.OnPropChanged();
            }
        }
    }

    public bool StateHasChanged
    {
        get => stateHasChanged;
        set
        {
            if (value != stateHasChanged)
            {
                stateHasChanged = value;
                this.OnPropChanged();
            }
        }
    }

    public RelayCommand DeleteMemberCommand => new(this.DeleteMember);
    public RelayCommand ChangeImageCommand => new(this.ChangeImage);
    public RelayCommand DeleteImageCommand => new(this.DeleteImage);
    public RelayCommand SaveCommand => new(this.Save);
    public RelayCommand CancelCommand => new(this.LoadData);

    public virtual ObservableCollection<string> Positions { get; set; }

    private void DeleteMember(object obj)
    {
        if (obj is Person person
            && MessageBox.Show($"Wollen Sie wirklich {person.Name} aus der Vorstandschaft entfernen?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            this.BoardMembers.Remove(person);
    }

    private void Save(object obj)
    {
        this.BoardMembers.ToList().ForEach(x =>
        {
            if (x.DeleteImage || !string.IsNullOrEmpty(x.UploadImagePath))
            {
                if (!string.IsNullOrEmpty(x.Bild))
                {
                    try
                    {
                        this.sftp.DeleteFile(SFTPProvider.BuildPath(Appsettings.Instance.RootDirectory, PathFragmentCollection.PersonImageDirectory, x.Bild));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Das Bild {x.Bild} konnte nicht gelöscht werden. Grund: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    x.Bild = string.Empty;
                }

                if (!string.IsNullOrEmpty(x.UploadImagePath))
                {
                    x.Bild = x.Name + Path.GetExtension(x.UploadImagePath);
                    this.sftp.UploadFileFromPath(
                        x.UploadImagePath,
                        SFTPProvider.BuildPath(Appsettings.Instance.RootDirectory, PathFragmentCollection.PersonImageDirectory, x.Bild));
                }
            }
        });

        this.sftp.UploadStringContent(this.JsonPath, JsonSerializer.Serialize(this.BoardMembers.ToArray()));
        this.StateHasChanged = false;
    }

    private void LoadData(object obj)
    {
        try
        {
            if (this.sftp is not null)
            {
                string boardStr = sftp.DownloadStringContent(this.JsonPath);
                boardMembers = JsonSerializer.Deserialize<ObservableCollection<Person>>(boardStr);
                boardMembers.ToList().ForEach(x => x.PropertyChanged += this.OnValueChanged);
                this.OnPropChanged(nameof(BoardMembers));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ChangeImage(object obj)
    {
        if (obj is Person pers)
        {
            OpenFileDialog ofd = new();
            ofd.Multiselect = false;
            ofd.Title = "Bild auswählen...";
            ofd.Filter = "Bilder (*.jpg, *.png, *.jpeg)|*.jpg; *.png; *.jpeg";
            if (ofd.ShowDialog() ?? false)
            {
                pers.UploadImagePath = ofd.FileName;
                pers.WpfImage = BitmapFrame.Create(new Uri(pers.UploadImagePath));
                pers.DeleteImage = false;
            }

        }
    }

    private void DeleteImage(object obj)
    {
        if (obj is Person pers)
        {
            pers.UploadImagePath = string.Empty;
            pers.DeleteImage = true;
            pers.WpfImage = null;
        }
    }

    private void OnValueChanged(object sender, PropertyChangedEventArgs args) => this.StateHasChanged = true;
}
