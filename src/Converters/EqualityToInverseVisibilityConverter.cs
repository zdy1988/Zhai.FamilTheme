using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    public class EqualityToInverseVisibilityConverter : ConverterMarkupExtensionBase<EqualityToInverseVisibilityConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.Equals(parameter)) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
