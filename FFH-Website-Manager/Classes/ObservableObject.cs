namespace FFH_Website_Manager.Classes;

using System.ComponentModel;
using System.Runtime.CompilerServices;

internal class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropChanged([CallerMemberName] string? src = null) => this.PropertyChanged?.Invoke(this, new (src));
}
