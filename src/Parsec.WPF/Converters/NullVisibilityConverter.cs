using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Parsec.WPF.Converters
{
    public class NullVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse(parameter as string, out var oposite);

            if (oposite)
                return value != null ? Visibility.Collapsed : Visibility.Visible;
            else
                return value is null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
