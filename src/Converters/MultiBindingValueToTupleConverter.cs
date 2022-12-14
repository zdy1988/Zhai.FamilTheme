using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class MultiBindingValueToTupleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch (values.Length)
            {
                case 1:
                    return new Tuple<object>(values[0]);
                case 2:
                    return new Tuple<object, object>(values[0], values[1]);
                case 3:
                    return new Tuple<object, object, object>(values[0], values[1], values[2]);
                case 4:
                    return new Tuple<object, object, object, object>(values[0], values[1], values[2], values[3]);
                case 5:
                    return new Tuple<object, object, object, object, object>(values[0], values[1], values[2], values[3], values[4]);
                case 6:
                    return new Tuple<object, object, object, object, object, object>(values[0], values[1], values[2], values[3], values[4], values[5]);
                case 7:
                    return new Tuple<object, object, object, object, object, object, object>(values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                default:
                    return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
