using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    public class UtcDateToLocalTimeStringConverter : ConverterMarkupExtensionBase<UtcDateToLocalTimeStringConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "unknown";
            var utcDate = (DateTime)value;
            return utcDate.ToLocalTime().ToString("f");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
