using FontAwesome.WPF;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace Parsec.WPF.Converters
{
    public class FileToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            var extension = Path.GetExtension(path);

            switch(extension)
            {
                case "":
                    return FontAwesomeIcon.Folder;

                default:
                    return FontAwesomeIcon.Question;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
