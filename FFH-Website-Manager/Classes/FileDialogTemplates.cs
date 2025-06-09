namespace FFH_Website_Manager.Classes;

using Microsoft.Win32;

static class FileDialogTemplates
{
    internal static bool SelectSingleImage(out string path)
    {
        OpenFileDialog ofd = new();
        ofd.Multiselect = false;
        ofd.Title = "Bild auswählen...";
        ofd.Filter = "Bilder (*.jpg, *.png, *.jpeg)|*.jpg; *.png; *.jpeg";
        bool result = ofd.ShowDialog() ?? false;
        path = ofd.FileName;
        return result;
    }
}
