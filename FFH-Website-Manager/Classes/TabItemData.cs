namespace FFH_Website_Manager.Classes;

using System.Windows.Controls;

internal class TabItemData(string header, UserControl content)
{
    public string Header { get; set; } = header;
    public UserControl Content { get; set; } = content;
}
