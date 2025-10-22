using FFH_Website_Manager.Classes.Model.Gallery;
using System.Globalization;
using System.Windows.Data;

namespace FFH_Website_Manager.Classes;

internal class UploadStateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is GalleryImage img)
            return img.IsAlreadyUploaded ? "☁︎ " + img.FileName : img.FileName;

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
