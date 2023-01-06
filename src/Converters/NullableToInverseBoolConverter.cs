using System;
using System.Globalization;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    public class NullableToInverseBoolConverter : ConverterMarkupExtensionBase<NullableToInverseBoolConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
