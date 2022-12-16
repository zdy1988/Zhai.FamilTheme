using System;
using System.Globalization;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class ByteLengthToStringConverter : ConverterMarkupExtensionBase<ByteLengthToStringConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long size)
            {
                return ConvertFileSize(size);
            }
            else
            {
                return "0B";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string ConvertFileSize(long size)
        {
            string outFileSize;

            if (size < 1024L)
            {
                outFileSize = String.Format("{0}B", size);
            }
            else if (size < 1024L * 1024L)
            {
                outFileSize = String.Format("{0}KB", size / 1024L);
            }
            else if (size < 1024L * 1024L * 1024L)
            {
                outFileSize = String.Format("{0}MB", size / (1024L * 1024L));
            }
            else if (size < 1024L * 1024L * 1024L * 1024L)
            {
                outFileSize = String.Format("{0}GB", size / (1024L * 1024L * 1024L));
            }
            else
            {
                outFileSize = String.Format("{0}TB", size / (1024L * 1024L * 1024L * 1024L));
            }

            return outFileSize;
        }
    }
}
