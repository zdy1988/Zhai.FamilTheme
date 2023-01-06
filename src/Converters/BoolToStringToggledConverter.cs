using System;
using System.Globalization;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    public class BoolToStringToggledConverter : ConverterMarkupExtensionBase<BoolToStringToggledConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            string[] strings = parameter.ToString().Split('-');

            return !(bool)value ? strings[0] : strings[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
