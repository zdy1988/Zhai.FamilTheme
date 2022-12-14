using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    internal class TransparentWindowTransparencyConverter : ConverterMarkupExtensionBase<TransparentWindowTransparencyConverter>, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2
                && values[0] is bool isTransparent
                && values[1] is double transparency
                && isTransparent)
            {
                return transparency;
            }

            return 1.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
