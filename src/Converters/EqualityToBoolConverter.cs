using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class EqualityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.Equals(parameter)) return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool r && r)
            {
                return parameter;
            }

            return null;
        }
    }
}
