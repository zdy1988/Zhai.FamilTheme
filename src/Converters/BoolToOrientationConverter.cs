using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class BoolToOrientationConverter: ConverterMarkupExtensionBase<BoolToOrientationConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if ((bool)value)
                return Orientation.Vertical;

            return Orientation.Horizontal;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
