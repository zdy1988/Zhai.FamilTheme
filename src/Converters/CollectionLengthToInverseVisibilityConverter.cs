using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class CollectionLengthToInverseVisibilityConverter : ConverterMarkupExtensionBase<CollectionLengthToInverseVisibilityConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a VisibilityProperty");

            if (value != null && !(((ICollection)value).Count > 0))
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
