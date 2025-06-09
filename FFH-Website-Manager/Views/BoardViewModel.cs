namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using System.Collections.ObjectModel;

internal class BoardViewModel : PersonsViewModelBase
{
    protected override string JsonPath { get; } = PathFragmentCollection.Board;

    public override ObservableCollection<string> Positions { get; set; } =
        [string.Empty,
        "1. Vorsitzender", "1. Vorsitzende",
        "2. Vorsitzender", "2. Vorsitzende",
        "Ehrenvorsitzender", "Ehrenvorsitzende",
        "1. Schriftführer", "1. Schriftführerin",
        "2. Schriftführer", "2. Schriftführerin",
        "1. Kassier", "2. Kassier",
        "Beisitzer", "Beisitzerin",
        "Datenschutzbeauftragter", "Datenschutzbeauftragte"];
}