using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FFH_Website_Manager.Classes;

class ObjectToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var type = value.GetType();
        return value is null || type.Namespace == "MS.Internal" ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
