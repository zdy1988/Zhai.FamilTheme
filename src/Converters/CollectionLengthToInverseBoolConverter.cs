using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class CollectionLengthToInverseBoolConverter: ConverterMarkupExtensionBase<CollectionLengthToInverseBoolConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((ICollection)value).Count > 0)
                return false;

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
