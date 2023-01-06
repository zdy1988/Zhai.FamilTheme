using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    public class CollectionLengthToBoolConverter : ConverterMarkupExtensionBase<CollectionLengthToBoolConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((ICollection)value).Count > 0)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
