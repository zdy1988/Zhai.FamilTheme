using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zhai.FamilTheme.Converters
{
    public class BoolToStringToggledConverter : IValueConverter
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
