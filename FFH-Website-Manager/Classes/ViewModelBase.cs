namespace FFH_Website_Manager.Classes;
internal abstract class ViewModelBase: ObservableObject
{
    protected readonly SFTPProvider sftp;

    protected ViewModelBase()
    {
        this.sftp = App.SFTPProvider;
    }
}
