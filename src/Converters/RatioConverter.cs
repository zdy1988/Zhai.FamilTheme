using System;
using System.Globalization;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    /// <summary>
    /// Convert double to double using a ratio parameter
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class RatioConverter : ConverterMarkupExtensionBase<RatioConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = 0d;
            if (value != null)
                size = System.Convert.ToDouble(value, CultureInfo.InvariantCulture) *
                       System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);

            return size;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
