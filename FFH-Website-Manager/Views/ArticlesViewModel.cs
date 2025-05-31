namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using FFH_Website_Manager.Popups;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

internal class ArticlesViewModel : ViewModelBase
{
    private ObservableCollection<Article> articles;

    public ArticlesViewModel() : base()
    {
        try
        {
            if (this.sftp is not null)
            {
                string articlesStr = sftp.DownloadStringContent("test/articles.json");
                Articles = [.. JsonSerializer.Deserialize<ObservableCollection<Article>>(articlesStr).OrderByDescending(x => x.DateInternal)];
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public RelayCommand EditArticleCommand => new RelayCommand(this.EditArticle);

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

    private void EditArticle(object article)
    {
        if (article is Article art)
        {
            using EditArticle ea = new EditArticle(art.Copy());
            ea.ShowDialog();

            if (ea.SaveData)
            {
                art.Insert(ea.Article);
                this.sftp.UploadStringContent("test/articles.json", JsonSerializer.Serialize(Articles.ToArray()));
            }
        }
    }
}
