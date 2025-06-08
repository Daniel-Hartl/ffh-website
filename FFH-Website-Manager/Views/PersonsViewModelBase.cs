namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;

internal abstract class PersonsViewModelBase : ViewModelBase
{
    private ObservableCollection<Person> boardMembers;
    protected virtual string JsonPath { get; }

    public PersonsViewModelBase() : base()
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
                boardMembers = value;
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
                    this.sftp.DeleteFile(SFTPProvider.BuildPath(Appsettings.Instance.RootDirectory, PathFragmentCollection.PersonImageDirectory, x.Bild));
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
    }

    private void LoadData(object obj)
    {
        try
        {
            if (this.sftp is not null)
            {
                string boardStr = sftp.DownloadStringContent(this.JsonPath);
                BoardMembers = JsonSerializer.Deserialize<ObservableCollection<Person>>(boardStr);
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
            pers.WpfImage = null;
            pers.UploadImagePath = string.Empty;
            pers.DeleteImage = true;
        }
    }
}
