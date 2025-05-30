using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace FFH_Website_Manager.Views;
internal class ArticlesViewModel : ViewModelBase
{
    private ObservableCollection<Article> articles;

    public ArticlesViewModel() : base()
    {
        try
        {
            string articlesStr = sftp.DownloadStringContent("test/articles.json");
            Articles = JsonSerializer.Deserialize<ObservableCollection<Article>>(articlesStr);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public ObservableCollection<Article> Articles
    {
        get => articles;
        set
        {
            if (value != articles)
            {
                articles = value;
                this.OnPropChanged();
            }
        }
    }
}
