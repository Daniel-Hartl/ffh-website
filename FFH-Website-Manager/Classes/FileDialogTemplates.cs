namespace FFH_Website_Manager.Classes;

using Microsoft.Win32;

static class FileDialogTemplates
{
    internal static bool SelectSingleImage(out string path)
    {
        OpenFileDialog ofd = new()
        {
            Multiselect = false,
            CheckFileExists = true,
            Title = "Bild auswählen...",
            Filter = "Bilder (*.jpg, *.png, *.jpeg)|*.jpg; *.png; *.jpeg"
        };
        bool result = ofd.ShowDialog() ?? false;
        path = ofd.FileName;
        return result;
    }

    internal static bool SelectMultipleImages(out List<string> paths)
    {
        OpenFileDialog ofd = new()
        {
            Multiselect = true,
            CheckFileExists = true,
            Title = "Bilder auswählen...",
            Filter = "Bilder (*.jpg, *.png, *.jpeg)|*.jpg; *.png; *.jpeg"
        };
        bool result = ofd.ShowDialog() ?? false;
        paths = [.. ofd.FileNames];
        return result;
    }
}
