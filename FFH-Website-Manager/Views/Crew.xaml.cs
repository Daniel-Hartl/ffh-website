using FFH_Website_Manager.Classes.Model;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FFH_Website_Manager.Views;
/// <summary>
/// Interaction logic for Crew.xaml
/// </summary>
public partial class Crew : UserControl
{
    public Crew()
    {
        InitializeComponent();
    }

    private void OpenImage(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ClickCount >= 2 && sender is Image img && img.DataContext is Person p)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "websiteManager", "imgPreview", Guid.NewGuid().ToString() + ".jpg");
            Directory.CreateDirectory(Path.GetDirectoryName(tempPath));

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(p.WpfImage);

            using (var fileStream = new FileStream(tempPath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = tempPath,
                UseShellExecute = true
            });
        }
    }
}
