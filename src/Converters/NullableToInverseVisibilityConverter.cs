using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    public class NullableToInverseVisibilityConverter : ConverterMarkupExtensionBase<NullableToInverseVisibilityConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;

            return (parameter != null && parameter.Equals("Hidden")) ? Visibility.Hidden : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
