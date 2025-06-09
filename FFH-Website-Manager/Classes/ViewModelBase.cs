namespace FFH_Website_Manager.Classes;
internal abstract class ViewModelBase: ObservableObject
{
    protected readonly SFTPProvider sftp;
    private bool stateHasChanged;

    protected ViewModelBase()
    {
        this.sftp = App.SFTPProvider;
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
