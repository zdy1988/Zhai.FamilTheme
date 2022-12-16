using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zhai.FamilTheme.Converters
{
    public class MultiBoolToBoolConverter : ConverterMarkupExtensionBase<MultiBoolToBoolConverter>, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            CultureInfo culture)
        {
            var param = parameter as string;

            if (string.IsNullOrEmpty(param))
                throw new ArgumentException();

            var boolean = param switch
            {
                "OR" => values.OfType<bool>().Aggregate(false, (current, value) => current || value),
                "AND" => values.OfType<bool>().Aggregate(true, (current, value) => current && value),
                _ => values.OfType<bool>().Aggregate(true, (current, value) => current && value),
            };

            return boolean;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
