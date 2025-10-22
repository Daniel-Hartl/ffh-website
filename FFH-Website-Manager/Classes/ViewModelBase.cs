using FFH_Website_Manager.Classes.DataProvider;

namespace FFH_Website_Manager.Classes;
internal abstract class ViewModelBase: ObservableObject
{
    protected readonly IDataProvider sftp;
    private bool stateHasChanged;

    protected ViewModelBase()
    {
        this.sftp = App.DataProvider;
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
    public RelayCommand SaveCommand => new(this.Save);
    public RelayCommand CancelCommand => new(this.LoadData);

    protected void StateChanging(object sender, System.ComponentModel.PropertyChangedEventArgs e) => this.StateHasChanged = true;

    protected virtual void Save(object obj) { }
    protected virtual void LoadData(object obj) { }
}
