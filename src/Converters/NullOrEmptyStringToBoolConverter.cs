using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zhai.FamilTheme.Converters
{
    public class NullOrEmptyStringToBoolConverter : ConverterMarkupExtensionBase<NullOrEmptyStringToBoolConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (value != null && value.ToString() != "")
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
