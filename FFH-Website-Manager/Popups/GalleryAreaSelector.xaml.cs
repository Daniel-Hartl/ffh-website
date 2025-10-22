using System.Windows;

namespace FFH_Website_Manager.Popups
{
    /// <summary>
    /// Interaction logic for GaleryAreaSelector.xaml
    /// </summary>
    public partial class GalleryAreaSelector : Window, IDisposable
    {
        public GalleryAreaSelector(bool isActiveInitial)
        {
            InitializeComponent();
            this.active.IsChecked = isActiveInitial;
            this.club.IsChecked = !isActiveInitial;
            this.DataContext = this;
        }

        public bool IsActiveSelected { get; set; }
        public bool Succeed { get; set; }

        private void Save(object sender, RoutedEventArgs e)
        {
            this.IsActiveSelected = this.active.IsChecked.GetValueOrDefault();
            this.Succeed = true;
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Dispose()
        {
        }
    }
}
