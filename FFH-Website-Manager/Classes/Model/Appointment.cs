using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FFH_Website_Manager.Classes.Model;
internal class Appointment : ObservableObject
{
    [JsonIgnore]
    private string titel;
    [JsonIgnore]
    private DateTime dateInternal;
    [JsonIgnore]
    private string ort;
    [JsonIgnore]
    private string zielgruppe;

    public string Zeit
    {
        get => DateInternal.ToString("dd.MM.yyyy-HH:mm");
        set => DateInternal = DateTime.ParseExact(value, "dd.MM.yyyy-HH:mm", null);
    }

    [JsonIgnore]
    public DateTime DateInternal
    {
        get => dateInternal;
        set
        {
            dateInternal = value;
            this.OnPropChanged();
        }
    }

    public string Titel
    {
        get => titel;
        set
        {
            titel = value;
            this.OnPropChanged();
        }
    }

    public string Ort
    {
        get => ort;
        set
        {
            ort = value;
            this.OnPropChanged();
        }
    }

    public string Zielgruppe
    {
        get => zielgruppe;
        set
        {
            zielgruppe = value;
            this.OnPropChanged();
        }
    }
}
