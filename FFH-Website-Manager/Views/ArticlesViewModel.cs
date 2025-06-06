namespace FFH_Website_Manager.Views;

using FFH_Website_Manager.Classes;
using FFH_Website_Manager.Classes.Model;
using FFH_Website_Manager.Popups;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

internal class ArticlesViewModel : ViewModelBase
{
    private ObservableCollection<Article> articles;

    public ArticlesViewModel() : base()
    {
        try
        {
            if (this.sftp is not null)
            {
                string articlesStr = sftp.DownloadStringContent(PathFragmentCollection.Articles);
                Articles = [.. JsonSerializer.Deserialize<ObservableCollection<Article>>(articlesStr).OrderByDescending(x => x.DateInternal)];
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler beim Laden der Daten vom Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public RelayCommand AddArticleCommand => new (this.AddArticle);
    public RelayCommand EditArticleCommand => new (this.EditArticle);
    public RelayCommand DeleteArticleCommand => new (this.DeleteArticle);

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

    private void AddArticle(object article)
    {
        Article art = new ();
        art.DateInternal = DateTime.Today;
        using EditArticle ea = new (art);
        ea.Title = "Artikel anlegen";
        ea.ShowDialog();

        if (ea.SaveData)
        {
            this.Articles.Add(ea.Article);
            this.sftp.UploadStringContent(PathFragmentCollection.Articles, JsonSerializer.Serialize(Articles.ToArray()));
        }
    }

    private void EditArticle(object article)
    {
        if (article is Article art)
        {
            using EditArticle ea = new (art.Copy());
            ea.Title = "Artikel bearbeiten";
            ea.ShowDialog();

            if (ea.SaveData)
            {
                art.Insert(ea.Article);
                this.sftp.UploadStringContent(PathFragmentCollection.Articles, JsonSerializer.Serialize(Articles.ToArray()));
            }
        }
    }

    private void DeleteArticle(object article)
    {
        if (article is Article art
            && MessageBox.Show($"Wollen Sie wirklich den Artikel \"{art.Titel}\" endgültig löschen?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        {
            this.Articles.Remove(art);
            this.sftp.UploadStringContent(PathFragmentCollection.Articles, JsonSerializer.Serialize(Articles.ToArray()));
        }
    }
}
