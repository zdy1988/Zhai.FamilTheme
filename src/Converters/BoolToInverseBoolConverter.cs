using System;
using System.Globalization;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    public class BoolToInverseBoolConverter : ConverterMarkupExtensionBase<BoolToInverseBoolConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}