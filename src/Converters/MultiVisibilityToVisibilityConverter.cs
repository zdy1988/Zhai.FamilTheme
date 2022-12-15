using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zhai.FamilTheme.Converters
{
    public class MultiVisibilityToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            CultureInfo culture)
        {
            var param = parameter as string;

            if (string.IsNullOrEmpty(param))
                throw new ArgumentException();

            bool[] boolValues = values.Select(t => (Visibility)t == Visibility.Visible ? true : false).ToArray();

            var visible = param switch
            {
                "OR" => boolValues.OfType<bool>().Aggregate(false, (current, value) => current || value),
                "AND" => boolValues.OfType<bool>().Aggregate(true, (current, value) => current && value),
                _ => boolValues.OfType<bool>().Aggregate(true, (current, value) => current && value),
            };

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
